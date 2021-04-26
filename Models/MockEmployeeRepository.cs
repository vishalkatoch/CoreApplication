using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _Employee;
        public MockEmployeeRepository()
        {
            _Employee = new List<Employee>
            {
                new Employee(){EmpId=101,Name="John",Deparment=Dept.Hr,Email="John@abc.com"},
                 new Employee(){EmpId=102,Name="Shawn",Deparment=Dept.IT,Email="Shawn@abc.com"},
                  new Employee(){EmpId=103,Name="Dawn",Deparment=Dept.Payroll,Email="Dawn@abc.com"}
            };
        }

        public Employee CreateEmployee(Employee employee)
        {
            employee.EmpId = _Employee.Max(t => t.EmpId)+1;
            try
            {
                _Employee.Add(employee);
            }
            catch(Exception ex)
            {
                return employee;
            }
            return employee;
        }

        public Employee Delete(int empId)
        {
            var empObj = _Employee.FirstOrDefault(e => e.EmpId == empId);
            if(empObj!=null)
            {
                _Employee.Remove(empObj);
            }
            return empObj;

        }

        public  IEnumerable<Employee> GetAllEmployee()
        {
            return _Employee;
        }

        public Employee GetEmployee(int Id)
        {
            return _Employee.FirstOrDefault(e => e.EmpId == Id);
        }

        public Employee Update(Employee employeeNW)
        {
            var empObj = _Employee.FirstOrDefault(e => e.EmpId == employeeNW.EmpId);
            if (empObj != null)
            {
                empObj.Name = employeeNW.Name;
                empObj.Email = employeeNW.Email;
                empObj.Deparment = employeeNW.Deparment;

            }
            return empObj;
        }
    }
}
