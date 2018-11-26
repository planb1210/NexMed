using NexMed.Data;
using NexMed.Entities;
using NexMed.WeatherServices;
using NexMed.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NexMed.Web.Controllers
{
    public class MemberController : Controller
    {
        [RoleAuthorize(RoleTypes.Member)]
        public ActionResult Index()
        {
            var user = (User)HttpContext.Items["User"];
            NexMedContext db = new NexMedContext();
            ViewBag.User = user;

            SelectList cities = new SelectList(db.Cities, "Id", "Name");
            ViewBag.Cities = cities;
            //ViewBag.Cities = db.Cities.Select(x=>x.Name).ToArray();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetWeather(int cityId)
        {
            Weatherbit ws = new Weatherbit();
            var weather = await ws.GetCityWeather(cityId);
            return Json(weather);
        }
    }
}