using NexMed.Data;
using NexMed.Entities;
using NexMed.WeatherServices;
using NexMed.Web.Filters;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NexMed.Web.Controllers
{
    public class MemberController : Controller
    {
        private NexMedContext db;
        private WeatherService weatherServices;

        public MemberController(NexMedContext context, WeatherService WeatherServices)
        {
            db = context;
            weatherServices = WeatherServices;
        }

        [RoleAuthorize(RoleTypes.Member)]
        public ActionResult Index()
        {
            var user = (User)HttpContext.Items["User"];
            ViewBag.User = user;

            SelectList cities = new SelectList(db.Cities, "Id", "Name");
            ViewBag.Cities = cities;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetWeather(int cityId)
        {
            var weather = await weatherServices.GetCityWeather(cityId);
            return Json(weather);
        }
    }
}