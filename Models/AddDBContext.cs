
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public class AddDBContext : IdentityDbContext
    {
        public AddDBContext(DbContextOptions<AddDBContext> options):base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            Random r = new Random();
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmpId = r.Next(2, 800000),
                    Name = "Ajax",
                    Deparment = Dept.IT,
                    Email = "Ajax@gr.com"
                }
                );
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}
