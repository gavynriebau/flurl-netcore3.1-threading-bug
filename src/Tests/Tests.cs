using Flurl.Http.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Web;
using Web.Controllers;
using Xunit;

namespace Tests
{
    public class Tests
    {
        /// <summary>
        /// This unit test passes
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UnitTest()
        {
            using (var httpTest = new HttpTest())
            {
                // Arrange
                const string expected = "expected-response";
                httpTest.RespondWith(expected);
                var controller = new WeatherForecastController();

                // Act
                var result = await controller.Get();

                // Assert
                Assert.Equal(expected, result);
            }
        }
    }

    public class IntegrationTests : IClassFixture<WebApplicationFactory<Web.Startup>>
    {
        private readonly WebApplicationFactory<Web.Startup> _factory;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// This integration test fails but should pass
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IntegrationTest()
        {
            using (var httpTest = new HttpTest())
            {
                // Arrange
                const string expected = "expected-response";
                httpTest.RespondWith(expected);
                var client = _factory.CreateClient();

                // Act
                var response = await client.GetAsync("/weatherforecast");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.Equal(expected, result);
            }
        }
    }
}
