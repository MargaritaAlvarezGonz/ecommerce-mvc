using Blossom_Web.Models.Dto;
using Blossom_Web.Models.Models;
using Blossom_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Blossom_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(LoginRequestDto model) 
        {
            return View();
        }

        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterRequestDto model)
        {
            var response = await _userService.Register<APIResponse>(model);
            if(response != null && response.IsExitoso)
            {
                return RedirectToAction("login");
            }
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult AccesDenied()
        {
            return View();
        }


    }
}
