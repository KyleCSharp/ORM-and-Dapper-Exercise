using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();
        void InstertDepartment(string newDepartmentName);
        
    }
}
