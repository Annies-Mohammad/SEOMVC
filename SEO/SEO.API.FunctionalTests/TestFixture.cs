using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;

namespace Nivo.API.FunctionalTest.TestSetup
{
    /// <summary>
    /// A test fixture which hosts the target project in an in-memory server.
    /// </summary>
    /// <typeparam name="Startup">Target project's startup type</typeparam>
    public class TestFixture<Startup> : IDisposable
    {
        #region Private fields

        /// <summary>
        /// Test server object instance
        /// </summary>
        private readonly TestServer _server;
        private readonly HttpClient _client;

        #endregion

        #region Public properties

        /// <summary>
        /// Client instance
        /// </summary>
        public HttpClient GetClient()
        {
            return _client;
        }
      
        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
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
