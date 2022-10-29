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
            IDbConnection connection = new MySqlConnection(connString);
            var repox = new DapperProductRepository(connection);
            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);
            TypeLine("Hello user, here are the current department");
            Console.WriteLine();
            TypeLine("please press enter. . . . . . .");
            Console.ReadLine();
            var depos = repo.GetAllDepartments();
            Print(repo.GetAllDepartments());

            TypeLine("Would you like to add a department? TYPE yes or jump to products type products");
            Console.WriteLine();
            var userReponse = Console.ReadLine();
            if (userReponse.ToLower()=="yes")
            {
                TypeLine("What is the name of your new department");
                Console.WriteLine();
                userReponse = Console.ReadLine();
                repo.InstertDepartment(userReponse);
                Print(repo.GetAllDepartments());
            }
            
            if (userReponse.ToLower() == "no")
            {

                TypeLine("LOGGING OUT");

                Environment.Exit(5000);



            }
            if(userReponse.ToLower() == "products")
            {
                PrintTwo(repox.GetAllProducts());
                Console.WriteLine("would you like to add a product?");
                var responseO = Console.ReadLine();
                if (responseO.ToLower() == "yes")
                {
                    Console.WriteLine("please add your product");

                    repox.CreateProduct(Console.ReadLine(), double.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
                    PrintTwo(repox.GetAllProducts());
                }
                if (responseO.ToLower() == "no")
                {
                    TypeLine("LOGGING OUT");

                    Environment.Exit(5000);
                }
            }
            TypeLine("would you like to delete a department *USE WITH CAUTION* PRESS ENTER ");
            Console.WriteLine();
            Console.ReadLine();
            

            
            var myString= "ARE YOU SURE PLEASE TYPE YES TO CONTINUE *USE WITH CAUTION";
            foreach (var character in myString)
            {
                Console.Write(character);
                Thread.Sleep(40);
            }
            Console.WriteLine();
            
            var userRepose_X = Console.ReadLine();
            if (userRepose_X.ToLower() == "yes")
            {
                Console.WriteLine("what department do you want to delete ID ONLY");
                int IdUserR = int.Parse(Console.ReadLine());
                repo.DeleteDepartment(IdUserR);
                Print(repo.GetAllDepartments());

            }
            else Console.WriteLine("Have a good day");

            TypeLine("would you like to look at products?");
            Console.WriteLine();

            
            var userRespose_Y = Console.ReadLine();
            if (userRespose_Y.ToLower() == "yes")
            {
                PrintTwo(repox.GetAllProducts());
            }
            if(userRespose_Y.ToLower()== "no")
            {
                TypeLine("LOGGING OUT");
                Environment.Exit(5000);

            }
            Console.WriteLine("would you like to add a product?");
            var response =Console.ReadLine();
            if (response.ToLower() == "yes")
            {
                Console.WriteLine("please add your product");
                
                repox.CreateProduct(Console.ReadLine(),double.Parse(Console.ReadLine()),int.Parse(Console.ReadLine()));
                PrintTwo(repox.GetAllProducts());
            }

            TypeLine("LOGGING OUT");


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
        static void TypeLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                System.Threading.Thread.Sleep(20); // Sleep for 150 milliseconds
            }
        }

        //prolly would be better if i did a switch will reform at a later date TBD
    }
}
