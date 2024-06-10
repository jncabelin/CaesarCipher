using System.ComponentModel;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace NominalSystem.Tests.MasterNodeAPI.IntegrationTests.Controllers
{
	public class MasterNodeController_Should: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public MasterNodeController_Should(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        [DisplayName("UseCase001_ReturnStatus500")]
        public async Task UseCase001_ReturnStatus500()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/decryptFile?fileName=Dummy");

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}

