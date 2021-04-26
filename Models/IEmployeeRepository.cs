using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployee();
        Employee CreateEmployee(Employee employee);
        Employee Update(Employee employeeNW);
        Employee Delete(int empId);
    }
}
