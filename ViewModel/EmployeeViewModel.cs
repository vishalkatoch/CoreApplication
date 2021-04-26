using CoreApplication.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.ViewModel
{
    public class EmployeeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Dept? Deparment { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
    }
}
