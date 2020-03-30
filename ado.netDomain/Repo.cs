using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ado.netDomain
{
    public class Repo
    {
        private static string addres = "https://www.bookclub.ua/catalog/books/fantasy_books/";
        public static void Parse()
        {
            HttpWebRequest reqw = (HttpWebRequest)HttpWebRequest.Create(addres);
        }


    }
}
