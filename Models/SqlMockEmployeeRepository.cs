using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public class SqlMockEmployeeRepository : IEmployeeRepository
    {
        private readonly AddDBContext _context;
        public SqlMockEmployeeRepository(AddDBContext context)
        {
            _context = context;
        }
        public Employee Add(Employee employeeNW)
        {
            _context.Employees.Add(employeeNW);
            _context.SaveChanges();
            return employeeNW;
        }

        public Employee CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Delete(int empId)
        {
            var empObj = _context.Employees.Find(empId);
            if(empObj!=null)
            {
                _context.Employees.Remove(empObj);
            }
            return empObj;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
           return  _context.Employees.FirstOrDefault(e => e.EmpId == Id);

        }

        public Employee Update(Employee employeeNW)
        {
         var objEmpUpdate=   _context.Employees.Attach(employeeNW);
            objEmpUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employeeNW;
        }
    }
}
