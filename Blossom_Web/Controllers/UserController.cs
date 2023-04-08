using Blossom_Utility;
using Blossom_Web.Models.Dto;
using Blossom_Web.Models.Models;
using Blossom_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

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

        public async Task<IActionResult> Login(LoginRequestDto model) 
        {
            var response = await _userService.Login<APIResponse>(model);
            if (response != null && response.IsExitoso == true)
            {
                LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, loginResponse.User.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role, loginResponse.User.Rol));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    
                    
                
                HttpContext.Session.SetString(DS.SessionToken, loginResponse.Token);
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                return View(model);
            }
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(DS.SessionToken, "");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccesDenied()
        {
            return View();
        }


    }
}
