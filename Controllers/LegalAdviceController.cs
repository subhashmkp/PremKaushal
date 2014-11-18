using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PremKaushal.Controllers
{
    public class LegalAdviceController : Controller
    {
        //
        // GET: /LegalAdvice/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Insert()
        {
            return View();
        }

    }
}
