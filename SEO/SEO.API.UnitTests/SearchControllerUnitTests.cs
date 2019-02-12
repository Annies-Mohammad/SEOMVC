using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SEO.API.Controllers;
using SEO.API.Models;
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

        [Theory]
        [InlineData("","www.infotrack.com.au")]
        [InlineData("","")]
        [InlineData("online title search","")]
        public void SearchUrl_Should_Throw_Error_On_Empty_Keyword(string searchTerm,string lookup)
        {
            //Act
            var model = new SearchViewModel
            {
                SearchTerm = searchTerm,
                Lookup = lookup
            };
           
            //Action
            var response = _controller.Get(model);

            //Assert
             _controller.Should().NotBeNull();
             response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Theory]
        [InlineData("online title search", "www.infotrack.com.au")]
        [InlineData("online title search", "infotrack")]
        public void SearchUrl_Should_Return_Valid_positions(string searchTerm, string lookup)
        {
            //Act
            var model = new SearchViewModel
            {
                SearchTerm = searchTerm,
                Lookup = lookup
            };

            //Action
            var response = _controller.Get(model);

            //Assert
            Assert.NotNull(_controller);
            Assert.Equal(typeof(ViewResult).FullName,response.GetType().FullName);
             
        }
    }
}
