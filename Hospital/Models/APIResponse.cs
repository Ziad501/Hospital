using System.Net;

namespace Hospital.Models
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessage { get; set; } = [];
        public object? Result { get; set; } = null;
        public HttpStatusCode StatusCode { get; set; }
    }
}
