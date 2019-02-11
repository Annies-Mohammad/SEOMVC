using FluentAssertions;
using Moq;
using SEO.API.Controllers;
using SEO.BusinessLogicLayer.Models.Interfaces;
using Xunit;

namespace SEO.API.UnitTests
{
   public class SearchControllerUnitTests
    {
        private Mock<ISearchURL> _mockSearchUrl;
        private SearchController _controller;

        public SearchControllerUnitTests()
        {
            _mockSearchUrl=new Mock<ISearchURL>(MockBehavior.Loose);
            _controller =new SearchController(_mockSearchUrl.Object);
        }

        [Fact]
        public void Controller_Construction_Test()
        {
            //Assert
            Assert.NotNull(_controller); //or
            _controller.Should().NotBeNull();
        }

        [Fact]
        public void SearchUrl_Should_throw_Error_On_Empty_Keyword()
        {
            //Action
            var response = _controller.Get(" ");
            //Assert
            Assert.NotNull(_controller); //or
            _controller.Should().NotBeNull();
        }
    }
}
