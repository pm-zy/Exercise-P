using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMLib.Controllers
{
    public class AdminNavController : Controller
    {
        //
        // GET: /AdminNav/

        public ActionResult Index()
        {
            if (Session["Name"] == null)
                return RedirectToAction("index", "Home");
            return View("Menu");
        }

    }
}
