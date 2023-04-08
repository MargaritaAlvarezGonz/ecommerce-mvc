using Blossom_Utility;
using Blossom_Web.Models;
using Blossom_Web.Models.Dto;
using Blossom_Web.Services.IServices;

namespace Blossom_Web.Services
{
    public class UserService : BaseService, IUserService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _blossomUrl;
        public UserService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient) 
        {
            _httpClient = httpClient;
            _blossomUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Login<T>(LoginRequestDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.POST,
                Data = dto,
                Url = _blossomUrl + "/api/v1/user/login"
            });
        }

        public Task<T> Register<T>(RegisterRequestDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.POST,
                Data = dto,
                Url = _blossomUrl + "/api/v1/user/register"
            });
        }
    }
}
