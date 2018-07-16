using System;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication.Test
{
    public class WebApplicationFactsBase : IDisposable
    {
        readonly HttpConfiguration configuration;
        readonly HttpServer msgHandler;

        public HttpClient Client { get; }

        public WebApplicationFactsBase()
        {
            configuration = new HttpConfiguration();
            new MyWebApplication().CreateWebApplication(configuration);
            msgHandler = new HttpServer(configuration);

            Client = new HttpClient(msgHandler);
        }

        public void Dispose()
        {
            Client.Dispose();
            msgHandler.Dispose();
            configuration.Dispose();
        }
    }
}