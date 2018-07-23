using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Aha.Models;
using Aha.ViewModels;


namespace Aha.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/tutors")]
        public ActionResult Tutors()
        {
            List<Tutor> allTutors = Tutor.GetAll();
            return View(allTutors);
        }

        [HttpGet("/clients")]
        public ActionResult Clients()
        {
            List<Client> allClients = Client.GetAll();
            return View(allClients);
        }

        [HttpGet("/specialties")]
        public ActionResult Specialties()
        {
            ViewModel newViewModel = new ViewModel();
            return View(newViewModel);
        }
    }
}
