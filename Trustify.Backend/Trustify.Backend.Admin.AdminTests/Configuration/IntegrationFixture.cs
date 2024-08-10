using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Trustify.Backend.Admin.AdminTests.Configuration
{
    public class IntegrationFixture : IDisposable
    {
        private const string BaseUri = "http://localhost:28802/api/v1.0/";
        private bool disposed = false;

        public WebApplicationFactory<Program> WebApplicationFactory { get; set; }

        public HttpClient HttpClient { get; set; }

        public IntegrationFixture()
        {
            WebApplicationFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                    });
                });

            HttpClient = WebApplicationFactory.CreateClient(
                new WebApplicationFactoryClientOptions { BaseAddress = new Uri(BaseUri) });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (!disposing)
            {
                return;
            }

            HttpClient.Dispose();
            WebApplicationFactory.Dispose();

            disposed = true;
        }
    }
}
