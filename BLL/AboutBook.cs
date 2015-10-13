using PMLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMLib.BLL
{
    public class AboutBook
    {

        /// <summary>
        /// 插入新书目BK
        /// </summary>
        /// <param name="bk"></param>
        /// <returns></returns>
        public bool AddBK(BK bk)
        {
            PMLibEntities pme = new PMLibEntities();
            try
            {
                pme.BK.Add(bk);
                return true;

            }
            catch
            {
                return false;
            }

        }


        /// <summary>
        /// 插入图书信息
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool AddBook(Book book)
        {
            PMLibEntities pme = new PMLibEntities();
            try
            {
                pme.Book.Add(book);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<BookInfo> GetReaderBookInfo()
        {
            PMLibEntities pme = new PMLibEntities();
           List <BookInfo> bList = new List<BookInfo>();
            var list = from q in pme.BK select q; 
            foreach(var li in list)
            {
                BookInfo bi = new BookInfo();
                bi.bkISBN=li.bkISBN;
                bi.bkName=li.bkName;
                bi.bkCount=li.bkCount;
                bi.bkAuthor=li.bkAuthor;
                bi.bkPublisher = li.bkPublisher;
                bi.bkPubTime = li.bkPubTime;
                bi.bkSort = li.bkSort;
                bi.inShelf =bi.bkCount- (from n in pme.LendBook where n.bkISBN== bi.bkISBN select n).Count();
                try
                {
                    bi.bookSite = (from s in pme.Book where s.bkISBN == li.bkISBN select s.bookSite).First();
                }
                catch
                {
                    bi.bookSite = "未知";
                }
                bList.Add(bi);
            }
            return bList;
           

        }
        //public bool UpdateBook()
    }
}