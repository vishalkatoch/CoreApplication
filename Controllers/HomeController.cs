using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Models;
using CoreApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CoreApplication.Controllers
{
    [Route("[Controller]/[action]")]
    public class HomeController: Controller
    {
        private IEmployeeRepository _employeeRepository;

        private IHostEnvironment HostingEnviorement { get; }

        public HomeController(IEmployeeRepository employeeRepository, IHostEnvironment hostingEnviorement)
        {
            _employeeRepository = employeeRepository;
            HostingEnviorement = hostingEnviorement;
        }
        [Route("")]
        [Route("~/")]

        public ActionResult Index()
        {
            return View(_employeeRepository.GetAllEmployee());
        }
        [Route("{id}")]
        public ActionResult Details(int? id)
        {
            var empFullListObj = _employeeRepository.GetEmployee(id ?? 1);
                if(empFullListObj==null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("EmployeeNotFound", id.Value);
            }
            return View(empFullListObj);
        }
        [HttpGet]
        public ActionResult EmployeeNotFound(int id)
        {
            return View(id);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                string filePath;
                if (model.Photo !=null)
                {
                    uniqueFileName = Guid.NewGuid().ToString()  + Path.GetExtension(model.Photo.FileName);
                    string updateFolder = Path.Combine(HostingEnviorement.ContentRootPath, @"wwwroot\Images");
                     filePath = Path.Combine(updateFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                }
                Employee empObj = new Employee()
                {
                    Name = model.Name,
                    Deparment = model.Deparment,
                    Email=model.Email,
                    PhotoUrl= uniqueFileName

                };

              var emp=  _employeeRepository.CreateEmployee(empObj);
                return RedirectToAction("details", new { id = emp.EmpId });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeViewModel = new EmployeeEditViewModel()
            {
                Id= employee.EmpId,
                Name = employee.Name,
                Deparment=employee.Deparment,
                Email=employee.Email,
                ExistingPhotoPath= employee.PhotoUrl

            };
            return View(employeeViewModel);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                string filePath;
                if (model.Photo != null)
                {
                    uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                    string updateFolder = Path.Combine(HostingEnviorement.ContentRootPath, @"wwwroot\Images");
                    filePath = Path.Combine(updateFolder, uniqueFileName);
                    using(var fileStream=new FileStream(filePath, FileMode.Create)){
                        model.Photo.CopyTo(fileStream);
                    }
                }
                Employee empObj = new Employee()
                {
                    EmpId=model.Id,
                    Name = model.Name,
                    Deparment = model.Deparment,
                    Email = model.Email,
                    PhotoUrl = uniqueFileName?? model.ExistingPhotoPath

                };

                var emp = _employeeRepository.Update(empObj);
                return RedirectToAction("details", new { id = emp.EmpId });
            }
            return View();
        }
    }
}
