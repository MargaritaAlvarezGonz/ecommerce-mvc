using AutoMapper;
using Blossom_Web.Models;
using Blossom_Web.Models.Dto;
using Blossom_Web.Models.Models;
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

        public async Task <IActionResult> Index()
        {
            List<BlossomDto> blossomList = new();
            var response = await _blossomService.GetAll<APIResponse>();
            if(response != null && response.IsExitoso) 
            {
                blossomList = JsonConvert.DeserializeObject<List<BlossomDto>>(Convert.ToString(response.Result));
            
            }
            return View(blossomList);
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