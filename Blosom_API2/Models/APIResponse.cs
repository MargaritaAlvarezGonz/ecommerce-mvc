using System.Net;

namespace Blosom_API2.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }

        public bool IsExitoso { get; set; } = true;

        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

    }
}
