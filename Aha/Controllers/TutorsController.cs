using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Aha.Models;
using Aha.ViewModels;


namespace Aha.Controllers
{
    public class TutorsController : Controller
    {
        [HttpGet("/tutors/new")]
        public ActionResult Form()
        {
            ViewModel newViewModel = new ViewModel();
            return View(newViewModel);
        }
    }
}