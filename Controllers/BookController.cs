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
    public class BookController : Controller
    {
        private PMLibEntities db = new PMLibEntities();

        //
        // GET: /Book/

        public ActionResult Index()
        {
            if (Session["Name"] == null)
                return RedirectToAction("index", "Home");
            var book = db.Book.Include(b => b.BK);
            return View(book.ToList());
        }

        //
        // GET: /Book/Details/5

        public ActionResult Details(int id = 0)
        {
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            ViewBag.bkISBN = new SelectList(db.BK, "bkISBN", "bkName");
            return View();
        }

        //
        // POST: /Book/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
             
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Book.Add(book);
                        db.SaveChanges();
                    }
                    catch { }
                    return RedirectToAction("Index");
                }

                ViewBag.bkISBN = new SelectList(db.BK, "bkISBN", "bkName", book.bkISBN);
                return View(book);
            
          
        }



        //
        // GET: /Book/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = (from i in db.Book where i.bookID==id select i).First() ;
            book.bookState = "下架";
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