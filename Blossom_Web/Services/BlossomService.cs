using Blossom_Utility;
using Blossom_Web.Models;
using Blossom_Web.Models.Dto;
using Blossom_Web.Services.IServices;

namespace Blossom_Web.Services
{
    public class BlossomService : BaseService, IBlossomService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _blossomUrl;

        public BlossomService(IHttpClientFactory httpClient, IConfiguration configuration): base(httpClient)
        {
            _httpClient = httpClient;
            _blossomUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T>Ceate<T>(BlossomCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.POST,
                Data = dto,
                Url = _blossomUrl+"/api/Blossom"

            });
        }

        public Task<T>Delete<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.DELETE,
                Url = _blossomUrl + "/api/Blossom/" + id

            });
        }

        public Task<T>Get<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.GET,
                Url = _blossomUrl + "/api/Blossom/" + id

            });
        }

        public Task<T>GetAll<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.GET,
                Url = _blossomUrl + "/api/Blossom" 

            });
        }

        public Task<T>Update<T>(BlossomUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.PUT,
                Data = dto,
                Url = _blossomUrl + "/api/Blossom/"+dto.Id

            });
        }
    }
        
}
