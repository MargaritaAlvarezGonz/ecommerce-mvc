using Blossom_Web.Models.Dto;

namespace Blossom_Web.Services.IServices
{
    public interface IUserService
    {
        Task<T>Login<T>(LoginRequestDto dto);
        Task<T>Register<T>(RegisterRequestDto dto);   
            
    }
}
