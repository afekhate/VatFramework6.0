using System.Net;

namespace FRSCInventory.Models.DataObjects.ApiModel
{
    public class ApiResponse
    {
        public string Message;
        public HttpStatusCode StatusCode;
        public object Content;
        public bool Success;

        public ApiResponse(string message, HttpStatusCode statusCode = HttpStatusCode.OK, object content = null, bool error = false)
        {
            Message = message;
            StatusCode = statusCode;
            Content = content;
            Success = error;
        }

        public ApiResponse()
        {

        }
    }
}
