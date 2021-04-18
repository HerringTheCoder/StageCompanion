using Moq;
using StageCompanion.Interfaces;
using StageCompanion.Services;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace StageCompanion.Testing
{
    public class HttpServiceTest
    {
        [Fact]
        public async Task TestIfApiIsCalledViaHttpServiceCorrectly()
        {
            var secureStorageMock = new Mock<ISecureStorage>();
            secureStorageMock.Setup(x => x.GetAsync(It.IsAny<string>()))
            .Returns(Task.FromResult("TestToken"));
            var httpService = new HttpService(secureStorageMock.Object);
            var response = await httpService.SendRequestAsync(HttpMethod.Get, "/");

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            secureStorageMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
