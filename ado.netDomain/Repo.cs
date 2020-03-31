using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ado.netDomain
{
    public class Repo
    {
        private static string addres = "https://www.bookclub.ua/catalog/books/fantasy_books/";
        public static string Parse()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(addres);//downcast, понижающее приведение
            request.UserAgent = "Mozilla/5.0";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();//создаем объект отклика


            HtmlParser parser = new HtmlParser();
            var document = parser.ParseDocument(response.GetResponseStream());
            var rows1 = document.QuerySelector(".main-topbooks .main-topbooks-book-deskr div.main-topbooks-book-name a");
            Console.WriteLine(rows1.InnerHtml);
            //string result;
            //using (StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("windows-1251")))
            //{
            //    var document2 = parser.ParseDocument(stream.ReadToEnd());
            //    var rows = document2.QuerySelector(".main-topbooks .main-topbooks-book-deskr div.main-topbooks-book-name a");
            //    Console.WriteLine(rows.InnerHtml);
            //    result = rows.InnerHtml;
            //    //parser.ParseDocument(stream.ReadToEnd());

            //    //result = stream.ReadToEnd();
            //}


            //"section.main-topbooks>*"
            //".main-topbooks-book-deskr div.main-topbooks-book-name a"
            //".main-topbooks .main-topbooks-book-deskr div.main-topbooks-book-name a"






            //var rows = document.QuerySelectorAll("section.main-topbooks>*");
            //foreach (var item in rows)
            //{
                
            //    Console.WriteLine(item.InnerHtml.ToString());

            //    using (FileStream fs = new FileStream("answer.txt", FileMode.Create, FileAccess.Write, FileShare.None))
            //    {
            //        using (StreamWriter writer = new StreamWriter(fs, Encoding.Unicode))
            //        {
            //            writer.WriteLine(item.InnerHtml);
            //        }
            //    }



            //    //Random r = new Random();
            //    //Console.ForegroundColor = (ConsoleColor)r.Next(1, 16);
            //    //Console.WriteLine(item.InnerHtml);
            //}



            //IElement element = document.QuerySelector(".main-topbooks");
            return rows1.InnerHtml;
        }
    }
}
