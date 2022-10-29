using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES(@name, @price, @categoryID);",
               new {name=name, price=price, categoryID = categoryID});
        }

        public IEnumerable<ProductClass> GetAllProducts()
        {
            return _connection.Query<ProductClass>("SELECT * FROM PRODUCTS;");
        }
    }
}
