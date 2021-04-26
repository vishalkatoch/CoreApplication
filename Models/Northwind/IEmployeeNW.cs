using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models.Northwind
{
   public interface IEmployeeNW
    {
        EmployeeNW GetEmployee(int Id);
        IEnumerable<EmployeeNW> GetAllEmployee();
        EmployeeNW Add(EmployeeNW employeeNW);
        EmployeeNW Update(EmployeeNW employeeNW);
        EmployeeNW Delete(int empId);
    }
}
