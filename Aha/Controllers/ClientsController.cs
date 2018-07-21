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
        public ActionResult Create(string clientFirstName, string clientLastName, string clientEmail, string clientPhoneNumber, string clientStreetAddress, string clientCity, string clientState, string clientZip, int clientAge)
        {
            Client newClient = new Client(clientFirstName, clientLastName, clientEmail, clientPhoneNumber, clientStreetAddress, clientCity, clientState, clientZip, clientAge);
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

        [HttpGet("/clients/{id}")]
        public ActionResult Details(int id)
        {
            ViewModel newViewModel = new ViewModel();
            newViewModel.FindClient(id);
            return View(newViewModel);
        }

        [HttpGet("/clients/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            Client currentClient = Client.Find(id);
            return View(currentClient);
        }

        [HttpPost("/clients/{id}/update")]
        public ActionResult Update(int id, string clientFirstName, string clientLastName, string clientEmail, string clientPhoneNumber, string clientStreetAddress, string clientCity, string clientState, string clientZip, int clientAge)
        {
            Client currentClient = Client.Find(id);
            currentClient.Update(clientFirstName, clientLastName, clientEmail, clientPhoneNumber, clientStreetAddress, clientCity, clientState, clientZip, clientAge, id);
            return RedirectToAction("Details", new { id = currentClient.Id});
        }

        [HttpGet("/clients/{id}/delete")]
        public ActionResult DeleteClientConfirmation(int id)
        {
            Client currentClient = Client.Find(id);
            return View(currentClient);
        }

        [HttpPost("/clients/{id}/delete")]
        public ActionResult DeleteClient(int id)
        {
            Client.Delete(id);
            return RedirectToAction("Clients", "Home");
        }

        [HttpGet("/clients/delete-all")]
        public ActionResult DeleteAllConfirmation(int id)
        {
            return View();
        }

        [HttpPost("/clients/delete-all")]
        public ActionResult DeleteAllClients()
        {
            Client.DeleteAll();
            return RedirectToAction("Clients", "Home");
        }

        [HttpPost("/clients/{clientId}/needs/{needId}/delete")]
        public ActionResult DeleteNeed(int clientId, int needId)
        {
            Client currentClient = Client.Find(clientId);
            Specialty currentNeed = Specialty.Find(needId);
            currentClient.DeleteNeed(currentNeed);
            return RedirectToAction("Needs", new { id = currentClient.Id});
        }
    }
}