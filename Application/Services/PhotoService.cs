namespace Application.Services;

[AutoInterface]
public class PhotoService : IPhotoService
{
    private const string RootLocation = "uploads";

    public async Task SavePhotoAsync(Guid id, Stream fileStream, string type)
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

    public async Task<Stream> GetPhotoAsync(Guid id, string type)
    {
            var location = type == "user"? $"users/{id}" : $"processes/{id}";
            var filePath = Path.Combine(RootLocation, location, $"{id}.jpg");

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(RootLocation, "profileIcon.jpg");
            }

            return await Task.FromResult(File.OpenRead(filePath));
    }
}