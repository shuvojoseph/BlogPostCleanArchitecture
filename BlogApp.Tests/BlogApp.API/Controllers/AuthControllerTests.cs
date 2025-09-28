// BlogApp.Tests/API/Controllers/AuthControllerTests.cs
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BlogApp.Tests.API.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }
        
        [Fact]
        public async Task Register_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = new RegisterRequest("test@example.com", "Password123!", "Test", "User");
            var expectedTokens = ("jwt-token", "refresh-token", DateTime.UtcNow.AddHours(1));

            _authServiceMock.Setup(x => x.RegisterAsync(request))
                .ReturnsAsync(expectedTokens);

            // Act
            var result = await _controller.Register(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Because controller returns the tuple directly, cast to ValueTuple
            var response = Assert.IsType<ValueTuple<string, string, DateTime>>(okResult.Value);

            Assert.Equal("jwt-token", response.Item1);
            Assert.Equal("refresh-token", response.Item2);
        }
        [Fact]
        public async Task Register_UserAlreadyExists_ReturnsBadRequest()
        {
            // Arrange
            var request = new RegisterRequest("test@example.com", "Password123!", "Test", "User");

            _authServiceMock.Setup(x => x.RegisterAsync(request))
                .ThrowsAsync(new ApplicationException("User already exists"));

            // Act
            var result = await _controller.Register(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User already exists", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var request = new LoginRequest("test@example.com", "Password123!");
            var expectedTokens = ("jwt-token", "refresh-token", DateTime.UtcNow.AddHours(1));

            _authServiceMock.Setup(x => x.LoginAsync(request))
                .ReturnsAsync(expectedTokens);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value;

            var responseType = response.GetType();
            var tokenProperty = responseType.GetProperty("Token");
            var refreshTokenProperty = responseType.GetProperty("RefreshToken");

            Assert.NotNull(tokenProperty);
            Assert.NotNull(refreshTokenProperty);

            var tokenValue = tokenProperty.GetValue(response) as string;
            var refreshTokenValue = refreshTokenProperty.GetValue(response) as string;

            Assert.Equal("jwt-token", tokenValue);
            Assert.Equal("refresh-token", refreshTokenValue);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var request = new LoginRequest("test@example.com", "WrongPassword");

            _authServiceMock.Setup(x => x.LoginAsync(request))
                .ThrowsAsync(new UnauthorizedAccessException("Invalid login attempt."));

            // Act
            var result = await _controller.Login(request);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid login attempt.", unauthorizedResult.Value);
        }
    }
}