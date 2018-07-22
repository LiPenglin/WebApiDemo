namespace WebApplication.Controllers
{
    public class CityFormattingService
    {
        public CityDto CreateCity(int x, int y, string cityName)
        {
            return new CityDto(x, y, cityName);
        }
    }
}