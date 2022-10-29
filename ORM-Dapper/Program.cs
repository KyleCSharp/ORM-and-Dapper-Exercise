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
            Thread.Sleep(5000);

             

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

            Console.WriteLine("would you like to look at products?");

            IDbConnection connection = new MySqlConnection(connString);
            var repox = new DapperProductRepository(connection);
            var userRespose_Y = Console.ReadLine();
            if (userRespose_Y == "yes")
            {
                PrintTwo(repox.GetAllProducts());
            }
            if(userRespose_Y == "no")
            {
                Console.WriteLine("logging out");
            }
            else Console.WriteLine("would you like to add a product?");
            var response =Console.ReadLine();
            if (response == "yes")
            {
                Console.WriteLine("please add your product");
                
                repox.CreateProduct(Console.ReadLine(),double.Parse(Console.ReadLine()),int.Parse(Console.ReadLine()));
                PrintTwo(repox.GetAllProducts());
            }
            
            
        }

        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($" ID: {depo.DepartmentID}  NAME : {depo.Name}");
            }
        }
        private static void PrintTwo(IEnumerable<ProductClass> deposx)
        {
            foreach (var product in deposx)
            {
             Console.WriteLine($" Product Name: {product.Name}          Product ID: {product.ProductID}        Category ID:  {product.CategoryID}        Product Price: {product.Price}       On sale 1 means yes: {product.OnSale}        Stock Level: {product.StockLevel}");
            }
        }
       
        //prolly would be better if i did a switch will reform at a later date TBD
    }
}
