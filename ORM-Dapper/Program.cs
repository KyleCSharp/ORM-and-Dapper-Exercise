using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

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

             foreach (var depo in depos)
             {
                Console.WriteLine($" ID: {depo.DepartmentID}  NAME : {depo.Name}");
             }
             // test
        }
    }
}
