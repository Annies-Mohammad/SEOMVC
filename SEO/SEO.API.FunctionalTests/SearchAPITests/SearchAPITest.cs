using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SEO.API.Controllers;
using SEO.BusinessLogicLayer.Models.Interfaces;
using Xunit;

namespace SEO.API.FunctionalTests.SearchAPITests
{
    public class SearchApiTest : IClassFixture<TestFixture<Startup>>
    {
        readonly HttpClient _client;
        private const string SearchKeyword = "InfoTrack";
        private readonly Mock<ISearchUrl> _mockSearchUrl;

        public SearchApiTest(TestFixture<Startup> fixture)
        {
            _client = fixture.GetClient();
            _mockSearchUrl = new Mock<ISearchUrl>();
        }

        [Fact]
        public void Search_Controller_should_Not_Be_Null()
        {
            //Act
            var searchCtrl = new SearchController(_mockSearchUrl.Object);
                searchCtrl.Should().NotBeNull();
        }

        [Fact]
        public async Task Search_Controller_Should_Return_Data()
        {
            //Act
            var url = $"/Search/Get/keywords?{SearchKeyword}";

            //Action
            var response = await _client.GetAsync(url).ConfigureAwait(false);
            var stringResult = await response.Content.ReadAsStringAsync();

            //Assert
            stringResult.Should().NotBeNullOrEmpty();
        }
    }
}
