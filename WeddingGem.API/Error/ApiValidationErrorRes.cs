namespace WeddingGem.API.Error
{
    public class ApiValidationErrorRes:ApiResponse
    {
        public IEnumerable<string> errors { get; set; }
        public ApiValidationErrorRes(List<string> error):base(400)
        {
            errors = error;
        }
    }
}
