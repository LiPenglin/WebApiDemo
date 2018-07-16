using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebApplication
{
    public class MyWebApplication   
    {
        public void CreateWebApplication(HttpConfiguration configuration)
        {
            HttpRouteCollection routes = configuration.Routes;
            routes.MapHttpRoute(
                "return Msg.",
                "Msg",
                new { controller = "Msg", action = "GetMsg" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            routes.MapHttpRoute(
                "return City",
                "City",
                new {controller = "City", action = "GetCity"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});

            routes.MapHttpRoute(
                "get city location by name",
                "CityLocation/{Name}&{Password}",
                new { controller = "City", action = "GetCityLocation" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            
            routes.MapHttpRoute(
                "post city",
                "CreateOneCity",
                new { controller = "City", action = "PostOneCity" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            routes.MapHttpRoute(
                "post city string",
                "CreateOneCityAsString/{CityString}",
                new {controller = "City", action = "PostOneCityAsString"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Post)});
            routes.MapHttpRoute(
                "post city query string",
                "CreateOneCityByQueryString",
                new {controller = "City", action = "PostOneCityAsQueryString"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Post)});
        }
    }
}