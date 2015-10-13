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
    public class ResController : Controller
    {
        private PMLibEntities db = new PMLibEntities();

        //
        // GET: /Res/

        public ActionResult Index()
        {
            var reserve = db.Reserve.Include(r => r.BK).Include(r => r.Reader);
            return View(reserve.ToList());
        }

       
        //
        // GET: /Res/Create

        public ActionResult Create()
        {
            ViewBag.bkISBN = new SelectList(db.BK, "bkISBN", "bkName");
            ViewBag.readerID = new SelectList(db.Reader, "readerID", "readerName");
            return View();
        }

        //
        // POST: /Res/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reserve reserve)
        {
            if (ModelState.IsValid)
            {
                db.Reserve.Add(reserve);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bkISBN = new SelectList(db.BK, "bkISBN", "bkName", reserve.bkISBN);
            ViewBag.readerID = new SelectList(db.Reader, "readerID", "readerName", reserve.readerID);
            return View(reserve);
        }

 

 

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}