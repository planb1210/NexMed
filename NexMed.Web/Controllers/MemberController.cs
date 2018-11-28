using NexMed.Data;
using NexMed.Entities;
using NexMed.Services;
using NexMed.WeatherServices;
using NexMed.Web.Filters;
using NexMed.Web.Helpers;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NexMed.Web.Controllers
{
    public class MemberController : Controller
    {
        private CityService cityService;
        private WeatherService weatherServices;

        public MemberController(CityService cService, WeatherService WeatherServices)
        {
            cityService = cService;
            weatherServices = WeatherServices;
        }

        [RoleAuthorize(RoleTypes.Member)]
        public ActionResult Index()
        {
            var user = HttpContext.GetUser();
            ViewBag.User = user;

            SelectList cities = new SelectList(cityService.GetCities(), "Id", "Name");
            ViewBag.Cities = cities;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetWeather(int cityId)
        {
            var city = cityService.GetCity(cityId);

            if (city != null)
            {
                var weather = await weatherServices.GetCityWeather(city);
                return Json(weather);
            }
            return Json(null);
        }
    }
}