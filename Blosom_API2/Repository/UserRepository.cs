using AutoMapper;
using Blosom_API2.Data;
using Blosom_API2.Models;
using Blosom_API2.Models.Dto;
using Blosom_API2.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blosom_API2.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        private readonly UserManager<UserAplication> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<UserAplication> userManager,
                              IMapper mapper, RoleManager<IdentityRole> roleManager) 
        {

            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager; 
            _mapper = mapper;
            _roleManager = roleManager;

        }
        public bool IsUniqueUser(string userName)
        {
            var user= _db.UserAplications.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _db.UserAplications.FirstOrDefaultAsync(u=> u.UserName.ToLower() ==loginRequestDTO.UserName.ToLower());
            
            bool isValido = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            
            if(user == null || isValido == false)

            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            //si el usuario existe generamos un jw token
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user),
                
            };
            return loginResponseDTO;
        }

        public async Task<UserDto> Register(RegistroRequestDTO registroRequestDTO)
        {
            UserAplication user = new()
            {
                UserName = registroRequestDTO.UserName,
                Email = registroRequestDTO.UserName.ToUpper(),
                Name = registroRequestDTO.Name,
                
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registroRequestDTO.Password);
                if (result.Succeeded)
                {

                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("cliente"));
                    }

                    await _userManager.AddToRoleAsync(user, "admin");
                    var userApp = _db.UserAplications.FirstOrDefault(u=>u.UserName == registroRequestDTO.UserName);
                    return _mapper.Map<UserDto>(userApp);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return new UserDto();

        }
    }
}
