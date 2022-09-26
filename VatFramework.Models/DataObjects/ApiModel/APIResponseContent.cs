using System.Net;

namespace FRSCInventory.Models.DataObjects.ApiModel
{
    public class APIResponseContent
    {
        public HttpStatusCode StatusCode { get; set; }

        public string ResponseMessage { get; set; }
        public object Data { get; set; }
    }
}
