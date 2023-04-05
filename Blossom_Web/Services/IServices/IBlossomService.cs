using Blossom_Web.Models.Dto;

namespace Blossom_Web.Services.IServices
{
    public interface IBlossomService
    {
        Task<T>GetAll<T>();
        Task<T> Get<T>(int id);
        Task<T> Ceate<T>(BlossomCreateDto dto);

        Task<T>Update<T>(BlossomUpdateDto dto);
        Task<T> Delete<T>(int id);
    }
}
