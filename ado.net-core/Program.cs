using System;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ado.net_core
{
    class Program
    {
        static void Main(string[] args)
        {
            //создание строки подключения к БД
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            //"Data Source=192.168.0.110,1433;Initial Catalog=BOOKSTORAGE;Persist Security Info=True;User ID=sa;password=Passw0rd%"
            sqlConnectionStringBuilder.UserID = @"sa";
            sqlConnectionStringBuilder.Password = @"Passw0rd%";
            sqlConnectionStringBuilder.InitialCatalog = @"BOOKSTORAGE";
            sqlConnectionStringBuilder.DataSource = @"192.168.0.110,1433";
            string connectionString = sqlConnectionStringBuilder.ToString();

            //создаем объект соединения
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                //открываем соединение
                sqlConnection.Open();
                //создаем команду обращения к процедуре в БД
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //sqlCommand.CommandText = "dbo.sp_getXML";
                sqlCommand.CommandText = "dbo.sp_getJSON";
                //запускаем команду и выводим ответ процедуры на экран
                string xmlString = (string)sqlCommand.ExecuteScalar();
                Console.WriteLine(xmlString);
                ////парсим в XML
                //XPathDocument resultXPathDocument = new XPathDocument(sqlCommand.ExecuteXmlReader());
                ////сохраняем в файл
                //using (XmlTextWriter xmlTextWriter = new XmlTextWriter(@"result.xml", Encoding.UTF8))
                //{
                //    //задаем кодировку
                //    xmlTextWriter.Formatting = Formatting.Indented;
                //    //перегоняем из буфера
                //    resultXPathDocument.CreateNavigator().WriteSubtree(xmlTextWriter);
                //}
                //Console.WriteLine("Recive an XML file.");
                ////создаем объект для работы с файлом
                //XDocument xDocument = new XDocument();
                ////загружаем файл
                //xDocument = XDocument.Load("result.xml");
                ////приклеиваем хэдер
                //xDocument.Declaration = new XDeclaration("1.0", "utf-8", "yes");
                ////созхаряем в файл
                //xDocument.Save("result.xml");
                //Console.WriteLine("Header added.");
                Console.ReadKey(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //закрываем соединение
                if (sqlConnection != null)
                    sqlConnection.Close();
            }

        }
    }
}
