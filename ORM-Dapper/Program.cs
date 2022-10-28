using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Config

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion 

            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);
            Console.WriteLine("Hello user, here are the current departments");
            Console.WriteLine("please press enter. . . . . . .");
            Console.ReadLine();
            var depos = repo.GetAllDepartments();
            Print(repo.GetAllDepartments());

             

            Console.WriteLine("Would you like to add a department?");
            var userReponse = Console.ReadLine();
            if (userReponse.ToLower()=="yes")
            {
                Console.WriteLine("What is the name of you new department");
                userReponse = Console.ReadLine();
                repo.InstertDepartment(userReponse);
                Print(repo.GetAllDepartments());
            }
            Console.WriteLine("would you like to delete a department *USE WITH CAUTION* PRESS ENTER ");
            Console.ReadLine();
            Console.WriteLine("ARE YOU SURE PLEASE TYPE YES TO CONTINUE *USE WITH CAUTION");
            

            var userRepose_X = Console.ReadLine();
            if (userRepose_X.ToLower() == "yes")
            {
                Console.WriteLine("what department do you want to delete ID ONLY");
                int IdUserR = int.Parse(Console.ReadLine());
                repo.DeleteDepartment(IdUserR);
                Print(repo.GetAllDepartments());

            }
            else Console.WriteLine("Have a good day");
            Console.WriteLine("Have a great day ");
            
        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($" ID: {depo.DepartmentID}  NAME : {depo.Name}");
            }
        }
       
        //prolly would be better if i did a switch will reform at a later date TBD
    }
}
