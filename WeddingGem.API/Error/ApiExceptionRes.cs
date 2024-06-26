using System.Net.NetworkInformation;

namespace WeddingGem.API.Error
{
    public class ApiExceptionRes:ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionRes(int status, string? message=null,string? details=null):base(status, message)
        {
            Details = details;
        }
    }
}
