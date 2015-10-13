using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMLib.Models;
using PMLib.BLL;

namespace PMLib.Controllers
{
    public class BKInfoController : Controller
    {
        private PMLibEntities db = new PMLibEntities();
        AboutBook aboutBook = new AboutBook();

        //
        // GET: /BKInfo/

        public ActionResult Index()
        {
            if (Session["Name"] == null)
                return RedirectToAction("index", "Home");
            List<BookInfo> list = new List<BookInfo>();
            list = aboutBook.GetReaderBookInfo();
            return View(list );
        }

        //
        // GET: /BKInfo/Details/5

        public ActionResult Details(string id = null)
        {
             BK bk = db.BK.Find(id);
                        List<BookInfo> list = new List<BookInfo>();
            list = aboutBook.GetReaderBookInfo();
            BookInfo bkInfo = (from i in list where i.bkISBN == bk.bkISBN select i).First(); 
            if (bk == null)
            {
                return HttpNotFound();
            }
            return View(bkInfo);
        }

        //
        // GET: /BKInfo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BKInfo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BK bk)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.BK.Add(bk);
                    db.SaveChanges();
                }
                catch { }
              
                return RedirectToAction("Index");
            }

            return View(bk);
        }

        //
        // GET: /BKInfo/Edit/5

        public ActionResult Edit(string id = null)
        {
            BK bk = db.BK.Find(id);
            if (bk == null)
            {
                return HttpNotFound();
            }
            return View(bk);
        }

        //
        // POST: /BKInfo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BK bk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bk);
        }

        //
        // GET: /BKInfo/Delete/5

        public ActionResult Delete(string id = null)
        {
            BK bk = db.BK.Find(id);
            if (bk == null)
            {
                return HttpNotFound();
            }
            return View(bk);
        }

        //
        // POST: /BKInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BK bk = db.BK.Find(id);
            db.BK.Remove(bk);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}