namespace Tests.Services;

[TestFixture]
public class PhotoUseCasesTests
{
    private IPhotoService _photoService;
    private string _testRootLocation;

    [SetUp]
    public void Setup()
    {
        _testRootLocation = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        Directory.CreateDirectory(Path.Combine(_testRootLocation, "users"));

        _photoService = new PhotoService();
        
        typeof(PhotoService)
            .GetField("_rootLocation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(_photoService, _testRootLocation);
    }

    [TearDown]
    public void Teardown()
    {
        if (Directory.Exists(_testRootLocation))
        {
            Directory.Delete(_testRootLocation, true);
        }
    }

    [Test]
    public async Task SavePhotoAsync_ShouldSaveFile_WhenInvoked()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var fileContent = Encoding.UTF8.GetBytes("test image content");
        using var fileStream = new MemoryStream(fileContent);

        // Act
        await _photoService.SavePhotoAsync(userId, fileStream, "user");

        // Assert
        var savedFilePath = Path.Combine(_testRootLocation, "users", userId.ToString(), $"{userId}.jpg");
        File.Exists(savedFilePath).Should().BeTrue();

        await using var savedFileStream = new FileStream(savedFilePath, FileMode.Open, FileAccess.Read, FileShare.None);
        var savedContent = new byte[savedFileStream.Length];
        await savedFileStream.ReadAsync(savedContent, 0, savedContent.Length);
        savedContent.Should().BeEquivalentTo(fileContent);
    }

    [Test]
    public async Task GetPhotoAsync_ShouldReturnPhotoStream_WhenPhotoExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var content = Encoding.UTF8.GetBytes("photo content");
        var userDir = Path.Combine(_testRootLocation, "users", userId.ToString());
        Directory.CreateDirectory(userDir);
        var filePath = Path.Combine(userDir, $"{userId}.jpg");
        await File.WriteAllBytesAsync(filePath, content);

        // Act
        await using var resultStream = await _photoService.GetPhotoAsync(userId, "user");

        // Assert
        resultStream.Should().NotBeNull();

        // Ensure the result stream is readable
        using var memoryStream = new MemoryStream();
        await resultStream.CopyToAsync(memoryStream);
        memoryStream.ToArray().Should().BeEquivalentTo(content);
    }
}