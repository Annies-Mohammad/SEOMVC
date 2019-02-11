using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SEO.API.Controllers;
using SEO.BusinessLogicLayer.Models.Interfaces;
using Xunit;

namespace SEO.API.UnitTests
{
   public class SearchControllerUnitTests
    {
        private Mock<ISearchUrl> _mockSearchUrl;
        private SearchController _controller;

        public SearchControllerUnitTests()
        {
            _mockSearchUrl=new Mock<ISearchUrl>(MockBehavior.Loose);
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
        public void SearchUrl_Should_Throw_Error_On_Empty_Keyword()
        {
            //Act
            var keyword = "";
            //Action
            var response = _controller.Get(keyword);

            //Assert
             _controller.Should().NotBeNull();
             response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
