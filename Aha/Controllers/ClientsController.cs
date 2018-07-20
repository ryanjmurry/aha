using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Aha.Models;
using Aha.ViewModels;


namespace Aha.Controllers
{
    public class ClientsController : Controller
    {
        [HttpGet("/clients/new")]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost("/clients/new")]
        public ActionResult Create(string clientFirstName, string clientLastName, string clientEmail, string clientPhoneNumber, string clientStreetAddress, string clientCity, string clientState, string clientZip, DateTime clientBirthday)
        {
            Client newClient = new Client(clientFirstName, clientLastName, clientEmail, clientPhoneNumber, clientStreetAddress, clientCity, clientState, clientZip, clientBirthday);
            newClient.Save();
            return RedirectToAction("Needs", new { id = newClient.Id});
        }

        [HttpGet("/clients/{id}/needs")]
        public ActionResult Needs(int id)
        {
            ViewModel newViewModel = new ViewModel();
            newViewModel.FindClient(id);
            return View(newViewModel);
        }

        [HttpPost("/clients/{id}/needs")]
        public ActionResult AddNeed(int id, int clientNeed)
        {
            Client currentClient = Client.Find(id);
            Specialty newNeed = Specialty.Find(clientNeed);
            currentClient.AddNeed(newNeed);
            return RedirectToAction("Needs");
        }
    }
}