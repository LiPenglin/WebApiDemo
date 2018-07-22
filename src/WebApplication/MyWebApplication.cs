using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using WebApplication.Controllers;

namespace WebApplication
{
    public class MyWebApplication   
    {
        public void CreateWebApplication(HttpConfiguration configuration)
        {
            IContainer rootScope = CreateContainer();
            ConfigRoutes(configuration);
            InitializeAdditionalProperties(configuration, rootScope);
            configuration.EnsureInitialized();
        }

        static IDependencyResolver InitializeAdditionalProperties(HttpConfiguration configuration, IContainer rootScope)
        {
            return configuration.DependencyResolver = new AutofacWebApiDependencyResolver(rootScope);
        }

        static void ConfigRoutes(HttpConfiguration configuration)
        {
            HttpRouteCollection routes = configuration.Routes;
            routes.MapHttpRoute(
                "return Msg.",
                "Msg",
                new {controller = "Msg", action = "GetMsg"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});

            routes.MapHttpRoute(
                "return City",
                "City",
                new {controller = "City", action = "GetCity"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});

            routes.MapHttpRoute(
                "get city location by name",
                "CityLocation/{Name}&{Password}",
                new {controller = "City", action = "GetCityLocation"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});

            routes.MapHttpRoute(
                "post city",
                "CreateOneCity",
                new {controller = "City", action = "PostOneCity"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Post)});

            routes.MapHttpRoute(
                "post city string",
                "CreateOneCityAsString",
                new {controller = "City", action = "PostOneCityAsString"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Post)});
            routes.MapHttpRoute(
                "post city query string",
                "CreateOneCityByQueryString",
                new {controller = "City", action = "PostOneCityAsQueryString"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Post)});
            routes.MapHttpRoute(
                "post city x",
                "PostCityX/{X}",
                new {controller = "City", action = "PostCityX"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Post)});
        }

        static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CityFormattingService>().InstancePerLifetimeScope();
            IContainer container = builder.Build();
            return container;
        }
    }
}