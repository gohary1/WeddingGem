namespace WeddingGem.Dashboard.Helper
{

    public class DocumentSettings
    {
      
            public static string UploadFile(IFormFile file, string folderName)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", folderName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);

                return $"/images/{folderName}/{fileName}";
            }

            public static bool DeleteFile(string ImagUrl)
            {
                if (ImagUrl.StartsWith("/"))
                {
                    ImagUrl = ImagUrl.Substring(1);
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ImagUrl);


                if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        return true;
                    }
                    return false;
            }

        }



}
