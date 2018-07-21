using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Aha.Models;

namespace Aha.Tests
{
    [TestClass]
    public class TutorTest : IDisposable
    {
        public void Dispose()
        {
            Tutor.DeleteAll();
            Client.DeleteAll();
            Appointment.DeleteAll();
            Specialty.DeleteAll();
        }

        public TutorTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=rjm_test;";
        }

        [TestMethod]
        public void GetSet_GetsAndSetsTutorProperties_Properties()
        {
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00, 1);
            Assert.AreEqual("Sean", newTutor.FirstName);
            Assert.AreEqual("Miller", newTutor.LastName);
            Assert.AreEqual("sm@gmail.com", newTutor.Email);
            Assert.AreEqual("1234567890", newTutor.PhoneNumber);
            Assert.AreEqual(1, newTutor.Experience);
            Assert.AreEqual(true, newTutor.Credential);
            Assert.AreEqual("Weekends", newTutor.Availability);
            Assert.AreEqual(25.00, newTutor.Rate);
            Assert.AreEqual(1, newTutor.Id);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfPropertiesAreSame_True()
        {
            Tutor newTutor1 = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00, 1);
            Tutor newTutor2 = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00, 1);
            Assert.AreEqual(newTutor1, newTutor2);
        }

        [TestMethod]
        public void GetAll_ChecksDbStartsEmpty_0()
        {
            int result = Tutor.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesTutorToDb_TutorList()
        {
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor.Save();
            List<Tutor> expectedList = new List<Tutor> { newTutor };
            List<Tutor> actualList = Tutor.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void DeleteAll_DeletesAllTutorsFromDb_TutorList()
        {
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor.Save();
            Tutor.DeleteAll();
            List<Tutor> expectedList = new List<Tutor> { };
            List<Tutor> actualList = Tutor.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Find_FindsTutorInDb_Tutor()
        {
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor.Save();
            Tutor foundTutor = Tutor.Find(newTutor.Id);
            Assert.AreEqual(newTutor, foundTutor);
        }

        [TestMethod]
        public void Delete_DeletesTutorInDb_TutorList()
        {
            Tutor newTutor1 = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor1.Save();
            Tutor newTutor2 = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor2.Save();
            Tutor.Delete(newTutor1.Id);
            List<Tutor> expectedList = new List<Tutor> { newTutor2 };
            List<Tutor> actualList = Tutor.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Update_UpdateTutorFromDb_String()
        {
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor.Save();
            newTutor.Update("Bill", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00, newTutor.Id);
            string actualFirstName = Tutor.Find(newTutor.Id).FirstName;
            Assert.AreEqual("Bill", actualFirstName);
        }

        [TestMethod]
        public void AddsGetsClients_AddsAndGetsAssociatedClientsFromDb_ClientList()
        {
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor.Save();
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient.Save();
            newTutor.AddClient(newClient);
            List<Client> expectedList = new List<Client> { newClient };
            List<Client> actualList = newTutor.GetClients();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GetsAppointments_GetsAssociatedAppointmentsFromDb_AppointmentList()
        {
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor.Save();
            DateTime time = new DateTime (1111, 11, 11);
            Appointment newAppointment = new Appointment(newTutor.Id, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345");
            newAppointment.Save();
            List<Appointment> expectedList = new List<Appointment> { newAppointment };
            List<Appointment> actualList = newTutor.GetAppointments();
            CollectionAssert.AreEqual(expectedList, actualList);

        }

        [TestMethod]
        public void AddsGetsSpecialtiess_AddsAndGetsAssociatedSpecialtiessFromDb_SpecialtyList()
        {
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor.Save();
            Specialty newSpecialty = new Specialty("Chemistry", "Science");
            newSpecialty.Save();
            newTutor.AddSpecialty(newSpecialty);
            List<Specialty> expectedList = new List<Specialty> { newSpecialty };
            List<Specialty> actualList = newTutor.GetSpecialties();
            CollectionAssert.AreEqual(expectedList, actualList);
        }
    }
}
