using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Aha.Models;

namespace Aha.Tests
{
    [TestClass]
    public class ClientTest : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }

        public ClientTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=rjm_test;";
        }

        [TestMethod]
        public void GetSet_GetsAndSetsClientProperties_Properties()
        {
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15, 1);
            Assert.AreEqual("Ashley", newClient.FirstName);
            Assert.AreEqual("Adelman", newClient.LastName);
            Assert.AreEqual("aa@gmail.com", newClient.Email);
            Assert.AreEqual("1234567890", newClient.PhoneNumber);
            Assert.AreEqual("123 ABC Street", newClient.StreetAddress);
            Assert.AreEqual("Xyz", newClient.City);
            Assert.AreEqual("ZZ", newClient.State);
            Assert.AreEqual("12345", newClient.Zip);
            Assert.AreEqual(15, newClient.Age);
            Assert.AreEqual(1, newClient.Id);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfPropertiesAreSame_True()
        {
            Client newClient1 = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15, 1);
            Client newClient2 = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15, 1);
            Assert.AreEqual(newClient1, newClient2);
        }

        [TestMethod]
        public void GetAll_ChecksDbStartsEmpty_0()
        {
            int result = Client.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesClientToDb_ClientList()
        {
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient.Save();
            List<Client> expectedList = new List<Client> { newClient };
            List<Client> actualList = Client.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void DeleteAll_DeletesAllClientsFromDb_ClientList()
        {
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient.Save();
            Client.DeleteAll();
            List<Client> expectedList = new List<Client> { };
            List<Client> actualList = Client.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Find_FindsClientInDb_Client()
        {
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient.Save();
            Client foundClient = Client.Find(newClient.Id);
            Assert.AreEqual(newClient, foundClient);
        }

        [TestMethod]
        public void Delete_DeletesClientInDb_ClientList()
        {
            Client newClient1 = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient1.Save();
            Client newClient2 = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient2.Save();
            Client.Delete(newClient1.Id);
            List<Client> expectedList = new List<Client> { newClient2 };
            List<Client> actualList = Client.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Update_UpdateClientFromDb_String()
        {
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15, 1);
            newClient.Save();
            newClient.Update("Bill", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15, newClient.Id);
            string actualFirstName = Client.Find(newClient.Id).FirstName;
            Assert.AreEqual("Bill", actualFirstName);
        }

        [TestMethod]
        public void AddsGetsTutors_AddsAndGetsAssociatedTutorsFromDb_TutorList()
        {
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient.Save();
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor.Save();
            newClient.AddTutor(newTutor);
            List<Tutor> expectedList = new List<Tutor> { newTutor };
            List<Tutor> actualList = newClient.GetTutors();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GetsAppointments_GetsAssociatedAppointmentsFromDb_AppointmentList()
        {
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient.Save();
            DateTime time = new DateTime (1111, 11, 11);
            Appointment newAppointment = new Appointment(1, newClient.Id, time, "123 ABC Street", "Xyz", "ZZ", "12345");
            newAppointment.Save();
            List<Appointment> expectedList = new List<Appointment> { newAppointment };
            List<Appointment> actualList = newClient.GetAppointments();
            CollectionAssert.AreEqual(expectedList, actualList);

        }

        [TestMethod]
        public void AddsGetsSpecialtiess_AddsAndGetsAssociatedSpecialtiessFromDb_SpecialtyList()
        {
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient.Save();
            Specialty newNeed = new Specialty("Chemistry", "Science");
            newNeed.Save();
            newClient.AddNeed(newNeed);
            List<Specialty> expectedList = new List<Specialty> { newNeed };
            List<Specialty> actualList = newClient.GetNeeds();
            CollectionAssert.AreEqual(expectedList, actualList);
        }
    }
}
