using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace SEO.API.FunctionalTests
{
    public class TestFixture<Startup> : IDisposable
    {
        #region Private fields

        /// <summary>
        /// Test server object instance
        /// </summary>
        private readonly TestServer _server;
        private readonly HttpClient _client;

        #endregion

        /// <summary>
        /// Client instance
        /// </summary>
        public HttpClient GetClient()
        {
            return _client;
        }

         

        #region Construction
        
        public TestFixture()
        {
            var builder = new WebHostBuilder()
                //.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
               // .UseIISIntegration()
                .UseStartup(typeof(Startup))
                .UseApplicationInsights();

             _server = new TestServer(builder);
            _client = _server.CreateClient();
            _client.Timeout = TimeSpan.FromSeconds(120);

            
        }

        #endregion

        #region Dispose methods

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        #endregion

    }
}
