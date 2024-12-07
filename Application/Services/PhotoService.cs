namespace Application.Services;

[AutoInterface]
public class PhotoService(ILogger<PhotoService> logger) : IPhotoService
{
    private const string RootLocation = "uploads";

    public async Task SavePhotoAsync(Guid id, Stream fileStream, string type)
     {
         try
         {
             var location = type == "user"? $"users/{id}" : $"processes/{id}";
             var userDir = Path.Combine(RootLocation, location);
             Directory.CreateDirectory(userDir);

             var filePath = Path.Combine(userDir, $"{id}.jpg");

             // Удаляем файл, если он уже существует
             if (File.Exists(filePath))
             {
                 File.Delete(filePath);
             }

             // Сохраняем новый файл
             await using var file = File.Create(filePath);
             await fileStream.CopyToAsync(file);
         }
         catch (Exception ex)
         {
             logger.LogError($"Не удалось сохранить фото {id}: {ex.Message}");
             throw;
         }
     }

    public async Task<Stream> GetPhotoAsync(Guid id, string type)
    {
        try
        {
            var location = type == "user"? $"users/{id}" : $"processes/{id}";
            var filePath = Path.Combine(RootLocation, location, $"{id}.jpg");

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(RootLocation, "profileIcon.jpg");
            }

            return await Task.FromResult(File.OpenRead(filePath));
        }
        catch (Exception ex)
        {
            logger.LogError($"Не удалось получить фото {id}: {ex.Message}");
            throw;
        }
    }
}