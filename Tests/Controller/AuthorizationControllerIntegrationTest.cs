namespace Tests.Controller;

    public class AuthorizationControllerIntegrationTest
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Program>(); 
            _client = factory.CreateClient(); 
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
        }

        [Test]
        public async Task Register_ValidRequest_ShouldReturnSuccess()
        {
            var request = new RegisterUserRequest
            {
                Login = "test_user_" + Guid.NewGuid().ToString().Substring(0, 16),
                Password = "password123",
                PasswordRepeat = "password123"
            };

            var response = await _client.PostAsJsonAsync("/Authorization/Register", request);

            Assert.That(response.IsSuccessStatusCode, Is.True, "Expected successful response.");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            
            var content = await response.Content.ReadAsStringAsync();
            
            Assert.NotNull(content);
            Assert.That(content, Is.EqualTo("Registration successful"));
        }
    }