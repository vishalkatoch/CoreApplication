using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CoreApplication.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public ActionResult ErrorHandlerPage(int statusCode)
        {
            switch(statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "The resource you are looking for cannot be found";
                    break;
                case 500:
                    var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

                    ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
                    ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
                    ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
                   
                    break;
            }
            return View();
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            // Retrieve the exception Details
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View("Error");
        }
    }


}