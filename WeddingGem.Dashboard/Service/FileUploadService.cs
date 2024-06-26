namespace WeddingGem.Dashboard.Service
{
    public class FileUploadService:IFileUploadService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FileUploadService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/WeddingGem/Files/Upload"; 
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(folderName), "folderName");
            content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

            var response = await _httpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<dynamic>();
            return result.filePath;
        }
    }
}
