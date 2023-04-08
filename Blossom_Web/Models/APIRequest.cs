using Microsoft.AspNetCore.Mvc;
using static Blossom_Utility.DS;

namespace Blossom_Web.Models
{
    public class APIRequest
    {
        public APIType APIType { get; set; } = APIType.GET;
        public string Url { get; set; }
        public object Data { get; set; }

        public string Token { get; set; }
        public Parameters Parameters { get; set; }
    }

    public class Parameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
 }
