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
            return View();
        }

        [HttpPost("/tutors/new")]
        public ActionResult Create(string tutorFirstName, string tutorLastName, string tutorEmail, string tutorPhoneNumber, int tutorExperience, bool tutorCredential, string tutorAvailability, double tutorRate)
        {
            Tutor newTutor = new Tutor(tutorFirstName, tutorLastName, tutorEmail, tutorPhoneNumber, tutorExperience, tutorCredential, tutorAvailability, tutorRate);
            newTutor.Save();
            return RedirectToAction("Specialties", new { id = newTutor.Id});
        }

        [HttpGet("/tutors/{id}/specialties")]
        public ActionResult Specialties(int id)
        {
            ViewModel newViewModel = new ViewModel();
            newViewModel.FindTutor(id);
            return View(newViewModel);
        }
    }
}