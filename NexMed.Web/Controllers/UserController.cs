using NexMed.Data;
using NexMed.Entities;
using NexMed.Services;
using NexMed.Web.Filters;
using NexMed.Web.Helpers;
using System.Web.Mvc;

namespace NexMed.Web.Controllers
{
    public class UserController : Controller
    {
        private UserService userService;
        private CityService cityService;

        public UserController(UserService uService, CityService cService)
        {
            userService = uService;
            cityService = cService;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            SelectList cities = new SelectList(cityService.GetCities(), "Id", "Name");
            ViewBag.Cities = cities;
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserRegisterModel user)
        {
            if (user.Password1 != user.Password2)
            {
                return HttpNotFound();
            }

            var isUserSet = userService.IsUserSet(user.Email);
            if (isUserSet)
            {
                return View();
            }

            var createdUser = userService.SetUser(user.City, user.Email, user.Name, user.Password1, (int)RoleTypes.Member);
            if (createdUser != null)
            {
                CookieUser.SetUser(createdUser);
                return RedirectToAction("Index", "Member");
            }

            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SignIn(string email, string password)
        {
            var user = userService.GetUser(email, password);
            if (user != null)
            {
                CookieUser.SetUser(user);
                return RedirectToAction("Index", "Member");
            }
            return View();
        }
    }
}