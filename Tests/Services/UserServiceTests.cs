namespace Tests.Services
{
    [TestFixture]
    public class UserServiceTests : BaseTest
    {
        private UserService _service;
        private Mock<IMapper> _mapperMock;
        private DbRepository _repository;
        private Mock<IBaseService> _baseServiceMock;
        private Mock<ISubscriptionService> _subscriptionServiceMock;

        [SetUp]
        public new void Setup()
        {
            base.Setup();
            _mapperMock = new Mock<IMapper>();
            _repository = new DbRepository(Context);
            _baseServiceMock = new Mock<IBaseService>();
            _subscriptionServiceMock = new Mock<ISubscriptionService>();
            
            _service = new UserService(_repository, _mapperMock.Object, _baseServiceMock.Object, _subscriptionServiceMock.Object);
        }

        [Test]
        public async Task ChangePassword_ShouldChangePasswordSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                Password = "hashedOldPassword",
                Salt = "userSalt",
                DateUpdated = DateTime.UtcNow
            };

            var request = new ChangePasswordRequest
            {
                Id = userId,
                PreviousPassword = "oldPassword",
                NewPassword = "newPassword"
            };

            _baseServiceMock.Setup(b => b.GetUserById(userId)).Returns(user);

            // Act
            await _service.ChangePassword(request);

            // Assert
            user.Password.Should().NotBe("hashedOldPassword"); // Verify password is updated
        }

        [Test]
        public async Task AddComment_ShouldAddCommentSuccessfully()
        {
            // Arrange
            var commentDto = new CommentDto
            {
                Id = Guid.NewGuid(),
                ProcessId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CommentText = "Sample comment"
            };

            var comment = new Comment
            {
                Id = commentDto.Id,
                Process = new Process(),
                User = new User(),
                CommentText = commentDto.CommentText
            };

            _mapperMock.Setup(m => m.Map<Comment>(commentDto)).Returns(comment);
            _mapperMock.Setup(m => m.Map<CommentDto>(It.IsAny<Comment>())).Returns(commentDto);

            // Act
            var result = await _service.AddComment(commentDto);

            // Assert
            result.Should().NotBeNull();
            result.CommentText.Should().Be(commentDto.CommentText);
        }

        [Test]
        public async Task AddIndicator_ShouldAddIndicatorAndNotifySubscribers()
        {
            // Arrange
            var indicatorDto = new IndicatorDto
            {
                Id = Guid.NewGuid(),
                ProcessId = Guid.NewGuid(),
                Name = "New Indicator"
            };

            var indicator = new Indicator
            {
                Id = indicatorDto.Id,
                Process = new Process(),
                Name = indicatorDto.Name
            };

            var subscribers = new List<Subscription>
            {
                new Subscription { User = new User { Id = Guid.NewGuid() } }
            };

            _mapperMock.Setup(m => m.Map<Indicator>(indicatorDto)).Returns(indicator);
            _subscriptionServiceMock.Setup(s => s.GetAllSubscribers(It.IsAny<Guid>())).ReturnsAsync(subscribers);

            // Act
            var result = await _service.AddIndicator(indicatorDto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(indicatorDto.Name);
            _subscriptionServiceMock.Verify(s => s.Notify(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task DeleteNotification_ShouldDeleteNotificationSuccessfully()
        {
            // Arrange
            var notificationId = Guid.NewGuid();
            var notification = new Notification { Id = notificationId };

            await Context.Notifications.AddAsync(notification);
            await Context.SaveChangesAsync();

            // Act
            await _service.DeleteNotification(notificationId);

            // Assert
            var deletedNotification = await Context.Notifications.FindAsync(notificationId);
            deletedNotification.Should().BeNull(); // Verify notification is deleted
        }

        [Test]
        public async Task ChangeLogin_ShouldUpdateLoginSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                Login = "oldLogin",
                DateUpdated = DateTime.UtcNow
            };

            var request = new ChangeLoginRequest
            {
                Id = userId,
                Login = "newLogin"
            };

            _baseServiceMock.Setup(b => b.GetUserById(userId)).Returns(user);
            
            // Act
            await _service.ChangeLogin(request);

            // Assert
            user.Login.Should().Be(request.Login); // Verify login is updated
        }

        [Test]
        public async Task GetUserById_ShouldReturnUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Login = "TestUser" };

            _baseServiceMock.Setup(b => b.GetUserById(userId)).Returns(user);

            var userDto = new UserDto { Id = userId, Login = "TestUser" };
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

            // Act
            var result = await _service.GetUserById(userId);

            // Assert
            result.Should().NotBeNull();
            result.Login.Should().Be(userDto.Login);
        }

        [Test]
        public async Task GetUserProcesses_ShouldReturnUserProcesses()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId };

            var company = new Company { User = user };
            var process = new Process { Company = company };

            await Context.Companies.AddAsync(company);
            await Context.Processes.AddAsync(process);
            await Context.SaveChangesAsync();

            _baseServiceMock.Setup(b => b.GetUserById(userId)).Returns(user);

            // Act
            var result = await _service.GetUserProcesses(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().Contain(p => p.Id == process.Id);
        }
    }
}
