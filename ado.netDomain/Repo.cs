using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ado.netDomain
{
    public class Repo
    {
        private static string address = "https://www.bookclub.ua/catalog/books/fantasy_books/";
        public static string Parse()
        {
            #region test
            //NuGet AngleSharp
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(addres);//downcast, понижающее приведение
            //request.UserAgent = "Mozilla/5.0";
            //request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();//создаем объект отклика

            //HtmlParser parser = new HtmlParser();
            //var document = parser.ParseDocument(response.GetResponseStream());
            //var rows1 = document.QuerySelector(".main-topbooks .main-topbooks-book-deskr div.main-topbooks-book-name a");
            //Console.WriteLine(rows1.InnerHtml);
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
            //return rows1.InnerHtml;
            #endregion

            #region test2

            return "";
            #endregion
        }

        public static DataTable DownloadDataFromDB()
        {
            //NuGet ConfigurationManager
            //формируем строку подключения из App.config
            string connectionString = ConfigurationManager.ConnectionStrings["LocalConnectToDB"].ConnectionString;


            //Создание строки подключения с помощью класса SqlConnectionStringBuilder
            //SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            //sqlConnectionStringBuilder.UserID = @"sa";
            //sqlConnectionStringBuilder.Password = @"Passw0rd%";
            //sqlConnectionStringBuilder.InitialCatalog = @"BOOKSTORAGE";
            //sqlConnectionStringBuilder.DataSource = @"192.168.0.110,1433";
            //string connectionString = sqlConnectionStringBuilder.ToString();

            //создаем объект соединения. NuGet System.Data.SqlClient;
            #region connentToDB1
            //SqlConnection sqlConnection = new SqlConnection(connectionString);
            //try
            //{
            //    //открываем соединение
            //    sqlConnection.Open();
            //    //создаем команду обращения к процедуре в БД
            //    SqlCommand sqlCommand = sqlConnection.CreateCommand();
            //    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //    //sqlCommand.CommandText = "dbo.sp_getXML";
            //    sqlCommand.CommandText = "dbo.sp_getJSON";
            //    //запускаем команду и выводим ответ процедуры на экран
            //    string xmlString = (string)sqlCommand.ExecuteScalar();
            //    Console.WriteLine(xmlString);
            //    ////парсим в XML
            //    //XPathDocument resultXPathDocument = new XPathDocument(sqlCommand.ExecuteXmlReader());
            //    ////сохраняем в файл
            //    //using (XmlTextWriter xmlTextWriter = new XmlTextWriter(@"result.xml", Encoding.UTF8))
            //    //{
            //    //    //задаем кодировку
            //    //    xmlTextWriter.Formatting = Formatting.Indented;
            //    //    //перегоняем из буфера
            //    //    resultXPathDocument.CreateNavigator().WriteSubtree(xmlTextWriter);
            //    //}
            //    //Console.WriteLine("Recive an XML file.");
            //    ////создаем объект для работы с файлом
            //    //XDocument xDocument = new XDocument();
            //    ////загружаем файл
            //    //xDocument = XDocument.Load("result.xml");
            //    ////приклеиваем хэдер
            //    //xDocument.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            //    ////созхаряем в файл
            //    //xDocument.Save("result.xml");
            //    //Console.WriteLine("Header added.");
            //    Console.ReadKey(true);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //finally
            //{
            //    //закрываем соединение
            //    if (sqlConnection != null)
            //        sqlConnection.Close();
            //}
            #endregion

            #region connectToDB2
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "SELECT * FROM [Books]";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                int line = 0;
                do
                {
                    while (sqlDataReader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                                dataTable.Columns.Add(sqlDataReader.GetName(i));
                            line++;
                        }
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            dataRow[i] = sqlDataReader[i];
                        dataTable.Rows.Add(dataRow);
                    }
                } while (sqlDataReader.NextResult());
            }
            return dataTable;
            #endregion
        }
        public static void ExecuteCommand(string text)
        {
            Console.WriteLine(text);
            //берем строкку подключения из App.config
            string connectionString = ConfigurationManager.ConnectionStrings["RemoteConnectToDB"].ConnectionString;
            //соединяемся с базой sql
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                //открываем соединение
                sqlConnection.Open();
                //создаем экземпляр для хранения команды
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                string name = @"KiriTwoL";
                string mail = @"Kiri@gmail.com";
                int level = 1;
                Console.WriteLine($"INSERT INTO [Users] ([Name], [Mail], [Level]) VALUES ({name}, {mail}, {level})");
                //записываем команду в экземпляр
                sqlCommand.CommandText = $"INSERT INTO [Users] ([Name], [Mail], [Level]) VALUES ('{name}', '{mail}', {level})";
                //посылаем запрос на бд
                Console.WriteLine($"{sqlCommand.ExecuteNonQuery()} rows were inserted");
                //соединение автоматически закрывается, по скольку мы использовали using
            }
        }

    }
}
