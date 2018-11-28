using NexMed.Data;
using NexMed.Entities;
using System;
using System.Linq;
using System.Web;

namespace NexMed.Web.Helpers
{
    public static class CookieUser
    {
        public static void SetUser(User user)
        {
            HttpContext.Current.Request.SetCookie(HttpContext.Current.Response, "nm_user_id", user.Id.ToString());
        }

        public static User GetUser()
        {
            var id = Convert.ToInt32(HttpContext.Current.Request.GetCookieValue("nm_user_id"));
            NexMedContext db = new NexMedContext();
            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}