using Blossom_Web.Models;
using Blossom_Web.Models.Models;

namespace Blossom_Web.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse responseModel { get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
