using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApplication.Controllers;
using Xunit;
using Xunit.Abstractions;

namespace WebApplication.Test
{
    public class WebApplicationFacts : WebApplicationFactsBase
    {
        readonly ITestOutputHelper output;

        public WebApplicationFacts(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task should_get_msg()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("http://www.bd.com/Msg");
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);

            MsgDto msgDto = await responseMessage.Content.ReadAsAsync<MsgDto>();
            Assert.Equal("Msg.", msgDto.Msg);
        }

        [Fact]
        public async Task should_get_msg_deserialization_serialization()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("http://www.bd.com/City");
            string cityString = await responseMessage.Content.ReadAsAsync<string>();
            CityDto city = JsonConvert.DeserializeObject<CityDto>(cityString);
            Assert.Equal("Beijing", city.CityName);
            Assert.Equal(50, city.X);
            Assert.Equal(50, city.Y);
        }

        [Fact]
        public void should_deserialize_citydto()
        {
//            var cityJson = "{\"cityName\": \"Beijing\"}";
//            var cityJson = "{\"cityName\": \"Beijing\", \"x\": 50, \"y\": 50}";
            var cityJson = "{\"cityName\": \"Beijing\", \"x\": 50, \"y\": 50, \"z\": 50}";
            var city = JsonConvert.DeserializeObject<CityDto>(cityJson);
            Assert.Equal("Beijing", city.CityName);
            Assert.Equal(50, city.X);
        }

        [Fact]
        public void should_deserialize_to_object()
        {
            var cityJson = "{\"cityname\": \"Beijing\", \"CityName\": \"Beijing Haidian\", \"x\": 50, \"y\": 50}";
            //            var city = JsonConvert.DeserializeObject(cityJson); // can not use d
            var anonymousType = JsonConvert.DeserializeAnonymousType(cityJson, new {CityName = default(string)});
//            Assert.Equal("Beijing", anonymousType.CityName);
        }

        [Fact]
        public void should_serialize_citydto()
        {
            var city = new CityDto(5, 5, "Beijing");
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string cityString = JsonConvert.SerializeObject(city, Formatting.None, settings);
            output.WriteLine(cityString);
        }


        [Fact]
        public async Task should_get_city_be_name()
        {
            HttpResponseMessage responseMsg = await Client.GetAsync("http://www.bd.com/CityLocation/Beijing&0x4396");
            string cityString = await responseMsg.Content.ReadAsAsync<string>();
            CityDto city = JsonConvert.DeserializeObject<CityDto>(cityString);

            Assert.Equal(50, city.X);
            Assert.Equal(50, city.Y);
        }

        // complex type from body
        [Fact]
        public async Task should_post_city()
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync(
                new Uri("http://bd.com/CreateOneCity"),
                new CityDto(58, 58, "Wuhan"));

            string cityString = await response.Content.ReadAsStringAsync();

            output.WriteLine(cityString);

            var city = JsonConvert.DeserializeObject<CityDto>(cityString);

            Assert.Equal(58, city.X);
            Assert.Equal(58, city.Y);
            Assert.Equal("Wuhan", city.CityName);
        }
        [Fact]
        public async Task should_post_null_city()
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync(
                new Uri("http://bd.com/CreateOneCity"),
                new CityDto());

            string cityString = await response.Content.ReadAsStringAsync();

            output.WriteLine(cityString);

            var city = JsonConvert.DeserializeObject<CityDto>(cityString);

            Assert.Equal(default(int), city.X);
            Assert.Equal(default(int), city.Y);
            Assert.Equal(default(string), city.CityName);
        }
        //complex type from uri
        [Fact]
        public async Task should_post_city_query_string_from_uri()
        {
            HttpResponseMessage response = await Client.PostAsync(
                new Uri("http://bd.com/CreateOneCityByQueryString/?X=101&Y=102&CityName=Shenzhen"),
                default(StringContent));
            string cityString = await response.Content.ReadAsStringAsync();
            output.WriteLine(cityString);
            var city = JsonConvert.DeserializeObject<CityDto>(cityString);

            Assert.Equal(101, city.X);
            Assert.Equal(102, city.Y);
            Assert.Equal("Shenzhen", city.CityName);
        }

        [Fact]
        public async Task should_post_part_city_query_string_from_uri()
        {
            HttpResponseMessage response = await Client.PostAsync(
                new Uri("http://bd.com/CreateOneCityByQueryString/?Y=102"),
                default(StringContent));
            string cityString = await response.Content.ReadAsStringAsync();
            output.WriteLine(cityString);
            var city = JsonConvert.DeserializeObject<CityDto>(cityString);

            Assert.Equal(default(int), city.X);
            Assert.Equal(102, city.Y);
            Assert.Equal(default(string), city.CityName);
        }
        //simple type from body
        [Fact]
        public async Task should_post_city_string()
        {
            HttpResponseMessage response = await Client.PostAsync(
                new Uri("http://bd.com/CreateOneCityAsString"),
                new StringContent(
                    "'Wuhan'", // JSON string
                    Encoding.UTF8,
                    "application/json"));


            string cityName = await response.Content.ReadAsStringAsync();
            output.WriteLine(cityName);
            Assert.Equal("\"Wuhan\"", cityName);
        }
        [Fact]
        public async Task should_post_city_x()
        {
            HttpResponseMessage response = await Client.PostAsync(
                new Uri("http://bd.com/PostCityX/101"),
                null);
            int x = await response.Content.ReadAsAsync<int>();
            Assert.Equal(101, x);
        }
        [Fact]
        public async Task should_post_city_null_x()
        {
            HttpResponseMessage response = await Client.PostAsync(
                new Uri("http://bd.com/PostCityX/"),
                null);
            HttpStatusCode code = response.StatusCode;
            Assert.Equal(HttpStatusCode.NotFound, code);
        }

        
    }
}