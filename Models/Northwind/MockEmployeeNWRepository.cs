using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models.Northwind
{
    public class MockEmployeeNWRepository : IEmployeeNW
    {
        private readonly AddDBContext _context;
        public MockEmployeeNWRepository(AddDBContext context)
        {
            _context = context;
        }
        public EmployeeNW Add(EmployeeNW employeeNW)
        {
            _context.Employees.Add(employeeNW);
            _context.SaveChanges();
            return employeeNW;
        }

        public EmployeeNW Delete(int empId)
        {
            var empObj = _context.Employees.Find(empId);
            if(empObj!=null)
            {
                _context.Employees.Remove(empObj);
            }
            return empObj;
        }

        public IEnumerable<EmployeeNW> GetAllEmployee()
        {
            return _context.Employees;
        }

        public EmployeeNW GetEmployee(int Id)
        {
           return  _context.Employees.FirstOrDefault(e => e.EmployeeID == Id);

        }

        public EmployeeNW Update(EmployeeNW employeeNW)
        {
         var objEmpUpdate=   _context.Employees.Attach(employeeNW);
            objEmpUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employeeNW;
        }
    }
}
