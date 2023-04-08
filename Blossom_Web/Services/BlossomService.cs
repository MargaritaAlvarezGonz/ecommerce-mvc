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

        public Task<T>Ceate<T>(BlossomCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.POST,
                Data = dto,
                Url = _blossomUrl+"/api/v1/Blossom",
                Token = token

            });
        }

        public Task<T>Delete<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.DELETE,
                Url = _blossomUrl + "/api/v1/Blossom/" + id,
                Token = token

            });
        }

        public Task<T>Get<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.GET,
                Url = _blossomUrl + "/api/v1/Blossom/" + id,
                Token = token

            });
        }

        public Task<T>GetAll<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.GET,
                Url = _blossomUrl + "/api/v1/Blossom",
                Token = token

            });
        }

        public Task<T> GetAllPaginated<T>(string token, int pageNumber = 1, int pageSize = 4)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.GET,
                Url = _blossomUrl + "/api/v1/Blossom/BlossomPaginated",
                Token = token,
                Parameters= new Parameters() { PageNumber = pageNumber,PageSize = pageSize }

            });
        }

        public Task<T>Update<T>(BlossomUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APIType.PUT,
                Data = dto,
                Url = _blossomUrl + "/api/v1/Blossom/"+dto.Id,
                Token = token

            });
        }
    }
        
}
