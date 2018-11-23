using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NexMed.Web.Controllers
{
    public class ErrorController : Controller
    {
        [ActionName("404")]
        public ActionResult Notfound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;
            return View("Notfound");
        }
    }
}