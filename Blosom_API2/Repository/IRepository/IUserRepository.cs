using Blosom_API2.Data;
using Blosom_API2.Models;
using Blosom_API2.Models.Dto;

namespace Blosom_API2.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string userName);

        Task<LoginResponseDTO> Login (LoginRequestDTO loginRequestDTO);
        Task<UserDto> Register(RegistroRequestDTO registroRequestDTO);
    }
}
