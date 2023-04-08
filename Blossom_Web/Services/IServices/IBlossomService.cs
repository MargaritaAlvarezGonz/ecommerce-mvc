using Blossom_Web.Models.Dto;

namespace Blossom_Web.Services.IServices
{
    public interface IBlossomService
    {
        Task<T>GetAll<T>(string token);
        Task<T>Get<T>(int id, string token);
        Task<T>Ceate<T>(BlossomCreateDto dto, string token);

        Task<T>Update<T>(BlossomUpdateDto dto, string token);
        Task<T>Delete<T>(int id, string token);
    }
}
