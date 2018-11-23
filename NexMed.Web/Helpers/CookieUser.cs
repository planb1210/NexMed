using NexMed.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NexMed.Web.Helpers
{
    public static class CookieUser
    {
        public static void SetUser(User user)
        {
            HttpContext.Current.Request.SetCookie(HttpContext.Current.Response, "nm_user_id", user.Id.ToString());
            HttpContext.Current.Request.SetCookie(HttpContext.Current.Response, "nm_user_name", user.Name.ToString());
            HttpContext.Current.Request.SetCookie(HttpContext.Current.Response, "nm_user_email", user.Email.ToString());
            HttpContext.Current.Request.SetCookie(HttpContext.Current.Response, "nm_user_city", user.City.ToString());
            HttpContext.Current.Request.SetCookie(HttpContext.Current.Response, "nm_user_role", user.Role.ToString());
        }

        public static User GetUser()
        {
            var user = new User()
            {
                Id = Convert.ToInt32(HttpContext.Current.Request.GetCookieValue("nm_user_id")),
                Name = HttpContext.Current.Request.GetCookieValue("nm_user_name"),
                Email = HttpContext.Current.Request.GetCookieValue("nm_user_email"),
                City = HttpContext.Current.Request.GetCookieValue("nm_user_city"),
                Role = Convert.ToInt32(HttpContext.Current.Request.GetCookieValue("nm_user_role"))
            };
            return user;
        }
    }
}