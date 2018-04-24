using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingSite.Controllers
{
    public class PhotosController : Controller
    {
        //
        // GET: /Photos/
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