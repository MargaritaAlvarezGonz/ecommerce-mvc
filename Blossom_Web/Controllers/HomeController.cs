using AutoMapper;
using Blossom_Utility;
using Blossom_Web.Models;
using Blossom_Web.Models.Dto;
using Blossom_Web.Models.Models;
using Blossom_Web.Models.ViewModel;
using Blossom_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Blossom_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlossomService _blossomService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IBlossomService blossomService, IMapper mapper)
        {
            _logger = logger;
            _blossomService = blossomService;
            _mapper = mapper;
        }
        public async Task <IActionResult> Index(int pageNumber=1)
        {
            List<BlossomDto> blossomList = new();
            BlossomPagesViewModel blossomVM= new BlossomPagesViewModel();

            if (pageNumber < 1) pageNumber = 1;

            var response = await _blossomService.GetAllPaginated<APIResponse>(HttpContext.Session.GetString(DS.SessionToken), pageNumber, 4);
            if(response != null && response.IsExitoso) 
            {
                blossomList = JsonConvert.DeserializeObject<List<BlossomDto>>(Convert.ToString(response.Result));
                blossomVM = new BlossomPagesViewModel()
                {
                    BlossomList = blossomList,
                    PageNumber = pageNumber,
                    TotalPages = JsonConvert.DeserializeObject<int>(Convert.ToString(response.TotalPages))
                };

                if (pageNumber > 1) blossomVM.Previous = "";
                if (blossomVM.TotalPages <= pageNumber) blossomVM.Next = "disabled";
            
            }
            return View(blossomVM);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}