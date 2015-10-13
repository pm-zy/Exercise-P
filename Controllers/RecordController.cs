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
    public class  PreKey
    {
        public int  readerID { get; set; }
        public int bookID { get; set; }
        public System.DateTime lendTime { get; set; }
    }
    public class RecordController : Controller
    {
        public List<int > GetAllinShelf()
        {
            List<int> list = (from i in db.Book where i.bookState == "在架" select i.bookID ).ToList();
            return list;
        }
        private PMLibEntities db = new PMLibEntities();
         
        //
        // GET: /Record/ 

        public ActionResult Index()
        {
            if (Session["Name"] == null)
                return RedirectToAction("index", "Home");
            var record = db.Record.Include(r => r.Reader);
            return View(record.ToList());
        }

        //
        // GET: /Record/Details/5

        public ActionResult Details(PreKey key)
        {
            int bid = key.bookID;
           int  rid = key.readerID;
            DateTime lendDate = key.lendTime;
            Record record = (from i in db.Record where i.readerID == rid && i.bookID == bid && i.lendTime == lendDate select i).First();
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }


        //续借
        public ActionResult Continue(PreKey key)
        {
            int bid = key.bookID;
            int  rid = key.readerID;
            DateTime lendDate = key.lendTime;
            Record record = (from i in db.Record where i.readerID == rid && i.bookID == bid && i.lendTime == lendDate select i).First();
            record.wellTime = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //
        // GET: /Record/Create

        public ActionResult Create()
        {
          //  ViewBag.readerID = new SelectList(db.Reader, "readerID", "readerName");
          //ViewBag.bookID = new SelectList(db.InShelfBook, "bookID", "bkName");
            return View();
        }

        //
        // POST: /Record/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Record record)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<int> list = GetAllinShelf();
                    var m = (from i in list where i == record.bookID select i).First();
                    //if(m==null)
                    //{

                    //}
                    db.Record.Add(record);
                    var n = (from e in db.Book where e.bookID == record.bookID select e).First();
                    n.bookState = "借出";
                    db.SaveChanges();
                }
                catch { }
               
                return RedirectToAction("Index");
            }

            ViewBag.readerID = new SelectList(db.Reader, "readerID", "readerName", record.readerID);
            return View(record);
        }

        ////
        //// GET: /Record/Edit/5

        //public ActionResult Edit(PreKey key)
        //{
        //    Record record = db.Record.Find(id);
        //    if (record == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.readerID = new SelectList(db.Reader, "readerID", "readerName", record.readerID);
        //    return View(record);
        //}

        ////
        //// POST: /Record/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Record record)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(record).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.readerID = new SelectList(db.Reader, "readerID", "readerName", record.readerID);
        //    return View(record);
        //}

        //
        // GET: /Record/Delete/5

        public ActionResult Delete(PreKey key )
        {
           int  bid = key.bookID;
           int  rid = key.readerID;
            DateTime lendDate = key.lendTime;
            Record record = (from i in db.Record where i.readerID == rid && i.bookID == bid && i.lendTime == lendDate select i).First();
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        //
        // POST: /Record/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(PreKey key)
        {
            int bid = key.bookID;
          int  rid = key.readerID;
            DateTime lendDate = key.lendTime;
            Record record = (from i in db.Record where i.readerID == rid && i.bookID == bid && i.lendTime == lendDate select i).First();
            record.returnTime = DateTime.Now ;
            var n = (from e in db.Book where e.bookID == record.bookID select e).First();
            n.bookState = "在架";
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