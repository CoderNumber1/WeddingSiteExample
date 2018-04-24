using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingSite.Controllers
{
    public class FaqController : Controller
    {
        //
        // GET: /Faq/
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }
	}
}