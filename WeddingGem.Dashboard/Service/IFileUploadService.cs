namespace WeddingGem.Dashboard.Service
{
    public interface IFileUploadService
    {
         Task<string> UploadFileAsync(IFormFile file, string folderName);
    }
}
