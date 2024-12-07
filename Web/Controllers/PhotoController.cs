namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotoController(IPhotoService photoService) : ControllerBase
{
    [HttpPost("HandleFileUpload/{id}")]
    public async Task<IActionResult> HandleFileUpload([FromRoute] Guid id, IFormFile file, [FromQuery] string type)
    {
        await photoService.SavePhotoAsync(id, file.OpenReadStream(), type);
        
        return Ok("Файл успешно загружен!");
    }

    [HttpGet("GetPhoto/{id}")]
    public async Task<IActionResult> GetPhoto([FromRoute] Guid id, [FromQuery] string type)
    {
        var photoStream = await photoService.GetPhotoAsync(id, type);
        
        return File(photoStream, "image/jpeg");
    }
}