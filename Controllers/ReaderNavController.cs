using PMLib.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMLib.Controllers
{
    public class ReaderNavController : Controller
    {
        //
        // GET: /ReaderNav/
        AboutBook aboutBook = new AboutBook();
        //public ActionResult Index()
        //{

           
        //    return View("ReaderBookInfo", aboutBook.GetReaderBookInfo());

        //}
        public ActionResult index( )
        {
 
     
            return View("ReaderBookInfo",aboutBook.GetReaderBookInfo() );

        }
        public ActionResult SearchString(String searchString)
        {
            List<BookInfo> list = new List<BookInfo>();
            list = aboutBook.GetReaderBookInfo();
            var li = from m in list select m;
            if (!String.IsNullOrEmpty(searchString))
            {

                li = li.Where(s => s.bkName.Contains(searchString));
            }
            return View("ReaderBookInfo", li);

        }
        public ActionResult SearchSort(String searchSort)
        {
            List<BookInfo> list = new List<BookInfo>();
            list = aboutBook.GetReaderBookInfo();
            var li = from m in list select m;
            if (!String.IsNullOrEmpty(searchSort))
            {

                li = li.Where(s => s.bkSort.Contains(searchSort));
            }
            return View("ReaderBookInfo", li);

        }
       
    }
}
