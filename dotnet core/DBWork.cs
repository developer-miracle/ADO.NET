using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace dotnet_core
{
    public static class DBWork
    {
        



        public static void ExecuteCommand(string text)
        {
            Console.WriteLine(text);
            //берем строкку подключения из App.config
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectToDB"].ConnectionString;
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