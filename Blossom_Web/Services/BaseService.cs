﻿using Blossom_Web.Models;
using Blossom_Web.Models.Models;
using Blossom_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using Blossom_Utility;
using System.Net.Http.Headers;
using System.Web;
using System.Net;
using System;

namespace Blossom_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get ; set ; }
        public IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient) 
        {
            this.responseModel = new ();
            _httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var cliente = _httpClient.CreateClient("BlossomAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");

                if(apiRequest.Parameters == null)
                {
                    message.RequestUri = new Uri(apiRequest.Url);
                }
                else
                {
                    var builder = new UriBuilder(apiRequest.Url);
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["PageNumber"] = apiRequest.Parameters.PageNumber.ToString();
                    query["PageSize"] = apiRequest.Parameters.PageSize.ToString();
                    builder.Query = query.ToString();
                    string url = builder.ToString();
                    message.RequestUri = new Uri(url);
                }

                

                if (apiRequest.Data !=null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                switch (apiRequest.APIType)
                {
                    case DS.APIType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case DS.APIType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    
                    case DS.APIType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                apiResponse= await cliente.SendAsync(message);
                var apiContent =await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    APIResponse response = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (response != null && (apiResponse.StatusCode == HttpStatusCode.BadRequest
                                        || apiResponse.StatusCode == HttpStatusCode.NotFound))
                    {
                        response.statusCode = HttpStatusCode.BadRequest;
                        response.IsExitoso = false;
                        var res = JsonConvert.SerializeObject(response);
                        var obj = JsonConvert.DeserializeObject<T>(res);
                        return obj;
                    }
                }  
                
            
                catch (Exception ex)
                {
                    var errorResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return errorResponse;
                }

                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;

            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsExitoso = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var responseEx = JsonConvert.DeserializeObject<T>(res);
                return responseEx;
            }
        }
    }
}
