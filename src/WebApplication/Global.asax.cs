using System;
using System.Web.Http;
using System.Web.Http.ModelBinding.Binders;

namespace WebApplication
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            new MyWebApplication().CreateWebApplication(GlobalConfiguration.Configuration);
        }
    }
}