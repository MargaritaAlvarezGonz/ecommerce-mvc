using Blosom_API2.Data;
using Blosom_API2.Models;
using Blosom_API2.Models.Dto;
using Blosom_API2.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blosom_API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private APIResponse _response;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _response = new();
        }

        [HttpPost("login")] //  /api/user/login
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("UserName or Password are not correct");
                return BadRequest(_response);
            }
            _response.IsExitoso = true;
            _response.statusCode = HttpStatusCode.OK;
            _response.Result = loginResponse;
            return Ok(_response);
        }


        [HttpPost("register")] //  /api/user/login
        public async Task<IActionResult> Register([FromBody] RegistroRequestDTO model)
        {
            bool isUniqueUser = _userRepo.IsUniqueUser(model.UserName);
            if (!isUniqueUser)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("User already exist!");
                return BadRequest(_response);
            }
            var user = await _userRepo.Register(model);
            if (user == null)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("Error registering user!");
                return BadRequest(_response);
            }
            _response.statusCode = HttpStatusCode.OK;
            _response.IsExitoso = true;
            return Ok(_response);
        }


    }
}
