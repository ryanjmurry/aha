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

        [HttpPost("/tutors/{id}/specialties")]
        public ActionResult AddSpecialty(int id, int tutorSpecialty)
        {
            Tutor currentTutor = Tutor.Find(id);
            Specialty newSpecialty = Specialty.Find(tutorSpecialty);
            currentTutor.AddSpecialty(newSpecialty);
            return RedirectToAction("Specialties");
        }

        [HttpGet("/tutors/{id}")]
        public ActionResult Details(int id)
        {
            ViewModel newViewModel = new ViewModel();
            newViewModel.FindTutor(id);
            return View(newViewModel);
        }

        [HttpGet("/tutors/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            ViewModel newViewModel = new ViewModel();
            newViewModel.FindTutor(id);
            return View(newViewModel);
        }

        [HttpPost("/tutors/{id}/update")]
        public ActionResult Update(int id, string tutorFirstName, string tutorLastName, string tutorEmail, string tutorPhoneNumber, int tutorExperience, bool tutorCredential, string tutorAvailability, double tutorRate)
        {
            Tutor currentTutor = Tutor.Find(id);
            currentTutor.Update(tutorFirstName, tutorLastName, tutorEmail, tutorPhoneNumber, tutorExperience, tutorCredential, tutorAvailability, tutorRate, id);
            return RedirectToAction("Details", new { id = currentTutor.Id});
        }

[       HttpGet("/tutors/delete-all")]
        public ActionResult DeleteAllConfirmation()
        {
            return View();
        }

        [HttpPost("/tutors/delete-all")]
        public ActionResult DeleteAll()
        {
            Tutor.DeleteAll();
            return RedirectToAction("Tutors", "Home");

        }

        [HttpGet("/tutors/{id}/delete")]
        public ActionResult DeleteTutorConfirmation(int id)
        {
            Tutor currentTutor = Tutor.Find(id);
            return View(currentTutor);
        }

        [HttpPost("/stylists/{id}/delete")]
        public ActionResult DeleteTutor(int id)
        {
            Tutor.Delete(id);
            return RedirectToAction("Tutors", "Home");
        }
    }
}