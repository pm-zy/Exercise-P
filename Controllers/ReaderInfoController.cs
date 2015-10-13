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
    public class ReaderInfoController : Controller
    {
        private PMLibEntities db = new PMLibEntities();

        //
        // GET: /ReaderInfo/

        public ActionResult Index()
        {
            if (Session["Name"] == null)
                return RedirectToAction("index", "Home");
            return View(db.Reader.ToList());
        }

        //
        // GET: /ReaderInfo/Details/5

        public ActionResult Details(int id = 0)
        {
            Reader reader = db.Reader.Find(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }

        //
        // GET: /ReaderInfo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReaderInfo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reader reader)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Reader.Add(reader);
                    db.SaveChanges();
                }
                catch { }
               
                return RedirectToAction("Index");
            }

            return View(reader);
        }

        //
        // GET: /ReaderInfo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Reader reader = db.Reader.Find(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }

        //
        // POST: /ReaderInfo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reader reader)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                    db.Entry(reader).State = EntityState.Modified;
                    db.SaveChanges();
                //}
                //catch { }
               
                return RedirectToAction("Index");
            }
            return View(reader);
        }

        //
        // GET: /ReaderInfo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Reader reader = db.Reader.Find(id);
            if (reader == null)
            {
                return HttpNotFound();
            }
            return View(reader);
        }

        //
        // POST: /ReaderInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reader reader = db.Reader.Find(id);
            db.Reader.Remove(reader);
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