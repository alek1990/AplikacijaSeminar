using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplikacija.Controllers
{
    public class ErrorController : Controller
    {

        // Globalne greške
        public ActionResult GlobalError()
        {
            return View();
        }

       // Error404 Not Found
        public ActionResult Error404()
        {
            return View();
        }
    }
}