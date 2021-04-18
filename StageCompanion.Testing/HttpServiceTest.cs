using Moq;
using StageCompanion.Services;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using StageCompanion.Services.Interfaces;
using Xunit;

namespace StageCompanion.Testing
{
    public class HttpServiceTest
    {
        private readonly Mock<ISecureStorage> _secureStorageMock;
        public HttpServiceTest()
        {
            _secureStorageMock = new Mock<ISecureStorage>();
            _secureStorageMock.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult("TestToken"));
        }

        [Theory]
        [InlineData("http://example.com")]
        [InlineData("https://stage-companion.herokuapp.com")]
        public async Task TestIfApiCanAccessPublicHosts(string hostname)
        {
            //TODO: Figure out the way to use Xamarin dependency injection without the use of 'GetService' anti-pattern or simple object construction
            var httpService = new HttpService(_secureStorageMock.Object, hostname);
            var response = await httpService.SendRequestAsync(HttpMethod.Get, "/");

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _secureStorageMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
