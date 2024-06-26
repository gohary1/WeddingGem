namespace WeddingGem.API.Error
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int status,string? message=null)
        {
            StatusCode = status;
            Message = message ?? getMessage(StatusCode);
        }
        public string? getMessage(int num)
        {
            return num switch
            {
                400 => "BadRequest",
                401 => "UnAuthorize",
                404 => "NotFound",
                500 => "ServerError"
            };
        }
    }
}
