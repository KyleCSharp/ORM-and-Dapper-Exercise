using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace ORM_Dapper
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _connection;


        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;

        }

        public void DeleteDepartment(string DeleteDepartmentName)
        {
            _connection.Execute("DELETE DEPARTMENTS(Name) VALUES(@depatmentname);");
            new {Departmentname = DeleteDepartmentName};
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            var depos = _connection.Query<Department>("SELECT * FROM departments").ToList();
            
            return depos;   

        }

        public void InstertDepartment(string  newDepartmentName)

        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
            new { departmentName = newDepartmentName });
        }
    }
}
