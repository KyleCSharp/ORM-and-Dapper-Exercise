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
            Print(repo.GetAllDepartments());



            Console.WriteLine("Would you like to add a department?");
            var userReponse = Console.ReadLine();
            if (userReponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of you new department");
                userReponse = Console.ReadLine();
                repo.InstertDepartment(userReponse);
                Print(repo.GetAllDepartments());
            }

            

        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($" ID: {depo.DepartmentID}  NAME : {depo.Name}");
            }
        }
    }
}
