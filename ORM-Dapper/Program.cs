using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Drawing.Printing;
using Google.Protobuf.Reflection;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Configuration 
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion

            
            IDbConnection connection = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(connection);

            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine($"please press enter...");
            Console.ReadLine();


            var depos = repo.GetAllDepartments();
            Print(depos);

            
           foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentId} Name: {depo.Name}");
            }

           
            Console.WriteLine($"do you want to add a department???");
            string userResponse = Console.ReadLine();

            

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new department?");
                userResponse = Console.ReadLine();
            

                repo.InsertDepartment(userResponse);
                Print (repo.GetAllDepartments());


            }

            Console.WriteLine("have a great day");
        }
       
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentId} Name: {depo.Name}");
            }
        }
   }
}
