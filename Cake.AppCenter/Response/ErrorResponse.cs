using System.Net;

using Cake.AppCenter.Enums;

namespace Cake.AppCenter.Response
{
    public class ErrorResponse
    {
        public ErrorDetails Error { get; set; }
    }

    public class ErrorDetails
    {
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ErrorCode Code { get; set; }
    }

}