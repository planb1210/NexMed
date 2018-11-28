using System.Linq;
using System.Web;
using System.Web.Mvc;
using NexMed.Web.Helpers;

namespace NexMed.Web.Filters
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly RoleTypes[] permissions;

        public RoleAuthorizeAttribute(params RoleTypes[] permissions)
        {
            this.permissions = permissions;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.GetUser();
            if (user != null)
            {
                return permissions.Where(x => (int)x == user.Role).Any();
            }
            return false;
        }
    }
}