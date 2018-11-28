using NexMed.Entities;
using System.Web;

namespace NexMed.Web.Helpers
{
    public static class HttpContextExtensions
    {
        public static User GetUser(this HttpContextBase httpContext)
        {
            return (User)httpContext.Items["User"];
        }
    }
}