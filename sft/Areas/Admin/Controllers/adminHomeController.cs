using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sft.Areas.Admin.Helpers;

namespace sft.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminHomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
          if (!User.IsAuthorized("news", "events", "rentals"))
          {
            return RedirectToAction("Index", "Home", new { area = ""});
          }
            return View();
        }

        
    }
}