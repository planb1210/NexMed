using NexMed.Data;
using NexMed.Entities;
using NexMed.Web.Filters;
using NexMed.Web.Helpers;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace NexMed.Web.Controllers
{
    public class UserController : Controller
    {
        private NexMedContext db;

        public UserController(NexMedContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            SelectList cities = new SelectList(db.Cities, "Id", "Name");
            ViewBag.Cities = cities;
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserRegisterModel user)
        {
            var password1 = getHash(user.Password1);
            var password2 = getHash(user.Password2);
            if (password1 != password2) {
                return RedirectToAction("404", "Error");
            }

            var isUserSet = db.Users.Where(x => x.Email == user.Email).Any();
            if (isUserSet) {
                return View();
            }

            var city = db.Cities.Where(x => x.Id == user.City).First();
            var newUser = new User() { City = city, Email = user.Email, Name = user.Name, Password = password1, Role = (int)RoleTypes.Member };
            
            db.Users.Add(newUser);
            db.SaveChanges();

            var createdUser = db.Users.Where(x => x.Email == user.Email).First();
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
            var newPassword = getHash(password);
            var user = db.Users.Where(x => x.Email == email && x.Password== newPassword).FirstOrDefault();
            if (user != null) {
                CookieUser.SetUser(user);
                return RedirectToAction("Index", "Member");
            }
            return View();
        }

        private string getHash(string value)
        {
            byte[] hash = Encoding.UTF8.GetBytes(value);
            MD5 md5 = new MD5CryptoServiceProvider();
            return Encoding.UTF8.GetString(md5.ComputeHash(hash));
        }
    }
}