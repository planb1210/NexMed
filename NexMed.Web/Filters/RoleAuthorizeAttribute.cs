using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NexMed.Entities;
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
            var user = (User)httpContext.Items["User"];
            return permissions.Where(x => (int)x == user.Role).Any();
        }
    }
}