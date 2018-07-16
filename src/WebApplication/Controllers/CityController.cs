using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebApplication.Controllers
{
    public class CityController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetCity()
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                "{'CityName': 'Beijing', 'X': 50, 'Y': 50}");
        }

        [HttpGet]
        public HttpResponseMessage GetCityLocation(
            [FromUri] string Name,
            [FromUri] string Password)
        {
            var cityList = new CityDto[]
            {
                new CityDto(50, 50, "Beijing"),
                new CityDto(53, 53, "Shanghai"),
                new CityDto(56, 56, "Guangzhou")
            };
            CityDto rescity = cityList.
                First(city => city.CityName.Equals(Name) && "0x4396".Equals(Password));
            return Request.CreateResponse(
                HttpStatusCode.OK,
                JsonConvert.SerializeObject(rescity));
        }

        [HttpPost]
        public HttpResponseMessage PostOneCity([FromBody] CityDto city)
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                city);
        }
        [HttpPost]
        public HttpResponseMessage PostOneCityAsString([FromUri]string cityString)
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                cityString);
            
        }

        [HttpPost]
        public HttpResponseMessage PostOneCityAsQueryString([FromUri] CityDto city)
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                city);
        }
    }
}