using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMLib.Models;

namespace PMLib.Controllers
{
    public class AlreadyLendController : Controller
    {
        private PMLibEntities db = new PMLibEntities();

        //
        // GET: /AlreadyLend/

        public ActionResult Index()
        {
            return View(db.LendBook.ToList());
        }

  
 

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}