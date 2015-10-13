using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMLib.BLL
{
    public class BookInfo
    {
        public string bkISBN { get; set; }
        public string bkName { get; set; }
        public string bkAuthor { get; set; }
        public string bkPublisher { get; set; }
        public Nullable<System.DateTime> bkPubTime { get; set; }//读者看不到这个
        public int bkCount { get; set; }//读者看不到这个
        public string bkSort { get; set; }
        public int inShelf { get; set; }
        public string bookSite { get; set; }
    }
}