using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Aha.Models;
using Aha.ViewModels;


namespace Aha.Controllers
{
    public class SpecialtiesController : Controller
    {
        [HttpGet("/specialties/{id}")]
        public ActionResult Matches()
        {
            ViewModel newViewModel = new ViewModel();
            newViewModel.FindSpecialty(id);
            return View(newViewModel);
        }
    }
}