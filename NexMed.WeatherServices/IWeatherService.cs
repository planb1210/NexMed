using NexMed.Entities;
using System.Threading.Tasks;

namespace NexMed.WeatherServices
{
    public interface IWeatherService
    {
        Task<Weather> GetCityWeather(int cityId);
    }
}
