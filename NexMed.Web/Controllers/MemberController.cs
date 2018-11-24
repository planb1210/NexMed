using NexMed.Entities;
using NexMed.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.User = user;
            return View();
        }
    }
}