namespace WebApplication.Controllers
{
    public class CityDto
    {
        public CityDto()
        {
        }

        public CityDto(int x, int y, string cityName)
        {
            X = x;
            Y = y;
            CityName = cityName;
        }

        public string CityName { get; set; }

        public int Y { get; set; }

        public int X { get; set; }
    }
}