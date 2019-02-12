using System;
using System.Net;
using Moq;
using SEO.BusinessLogicLayer.Models.Implementation;
using SEO.WorkerService.Interfaces;
using Xunit;

namespace SEO.BusinessLogicLayer.UnitTests
{
    public class SEOBLLUnitTests
    {
        private SearchUrl _searchUrl;
        readonly Mock<ISEORequestService> _mockSEORequestService;

        public SEOBLLUnitTests()
        {
            _mockSEORequestService = new Mock<ISEORequestService>();
            _searchUrl=new SearchUrl(_mockSEORequestService.Object);
        }

        [Fact]
        public void Constructor()
        {
            Assert.NotNull(_searchUrl);
            Assert.IsType<SearchUrl>(_searchUrl);
        }

        [Fact]
        public void Should_Get_Positions()
        {
            //Act
            var searchterm = "test";
            var lookup = "testlookup";

            var req = new Mock<HttpWebRequest>();

            _mockSEORequestService.Setup(request => request.CreateRequest(It.IsAny<string>())).Returns(req.Object);
            _mockSEORequestService
                .Setup(response => response.GetResponse(It.IsAny<HttpWebRequest>(), It.IsAny<string>()))
                .Returns("1,2");

            //Action
            var result=_searchUrl.GetSearchUrls(searchterm, lookup);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("1,2",result);
        }
    }
}
