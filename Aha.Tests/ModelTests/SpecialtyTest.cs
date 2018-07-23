using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Aha.Models;

namespace Aha.Tests
{
    [TestClass]
    public class SpecialtyTest : IDisposable
    {
       public void Dispose()
        {
            Specialty.DeleteAll();
        }

        public SpecialtyTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ryan_murry_test;";
        }

        [TestMethod]
        public void GetSet_GetsAndSetsSpecialtyProperties_Properties()
        {
            Specialty newSpecialty = new Specialty("Chemistry", "Science", 1);
            Assert.AreEqual("Chemistry", newSpecialty.Subject);
            Assert.AreEqual("Science", newSpecialty.Discipline);
            Assert.AreEqual(1, newSpecialty.Id);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfPropertiesAreSame_True()
        {
            Specialty newSpecialty1 = new Specialty("Chemistry", "Science", 1);
            Specialty newSpecialty2 = new Specialty("Chemistry", "Science", 1);
            Assert.AreEqual(newSpecialty1, newSpecialty2);
        }

        [TestMethod]
        public void GetAll_ChecksDbStartsEmpty_0()
        {
            int result = Specialty.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesSpecialtyToDb_SpecialtyList()
        {
            Specialty newSpecialty = new Specialty("Chemistry", "Science");
            newSpecialty.Save();
            List<Specialty> expectedList = new List<Specialty> { newSpecialty };
            List<Specialty> actualList = Specialty.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void DeleteAll_DeletesAllSpecialtysFromDb_SpecialtyList()
        {
            Specialty newSpecialty = new Specialty("Chemistry", "Science");
            newSpecialty.Save();
            Specialty.DeleteAll();
            List<Specialty> expectedList = new List<Specialty> { };
            List<Specialty> actualList = Specialty.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Find_FindsSpecialtyInDb_Specialty()
        {
            Specialty newSpecialty = new Specialty("Chemistry", "Science");
            newSpecialty.Save();
            Specialty foundSpecialty = Specialty.Find(newSpecialty.Id);
            Assert.AreEqual(newSpecialty, foundSpecialty);
        }

        [TestMethod]
        public void Delete_DeletesSpecialtyInDb_SpecialtyList()
        {
            Specialty newSpecialty1 = new Specialty("Chemistry", "Science");
            newSpecialty1.Save();
            Specialty newSpecialty2 = new Specialty("Chemistry", "Science");
            newSpecialty2.Save();
            Specialty.Delete(newSpecialty1.Id);
            List<Specialty> expectedList = new List<Specialty> { newSpecialty2 };
            List<Specialty> actualList = Specialty.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Update_UpdateSpecialtyFromDb_String()
        {
            Specialty newSpecialty = new Specialty("Chemistry", "Science");
            newSpecialty.Save();
            newSpecialty.Update("Biology", "Science", newSpecialty.Id);
            string actualSubject = Specialty.Find(newSpecialty.Id).Subject;
            Assert.AreEqual("Biology", actualSubject);
        }

        [TestMethod]
        public void AddsGetsClients_AddsAndGetsAssociatedClientsFromDb_ClientList()
        {
            Specialty newSpecialty = new Specialty("Chemistry", "Science");
            newSpecialty.Save();
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", 15);
            newClient.Save();
            newSpecialty.AddClient(newClient);
            List<Client> expectedList = new List<Client> { newClient };
            List<Client> actualList = newSpecialty.GetClients();
            CollectionAssert.AreEqual(expectedList, actualList);
        } 

        [TestMethod]
        public void AddsGetsTutors_AddsAndGetsAssociatedTutorsFromDb_TutorList()
        {
            Specialty newSpecialty = new Specialty("Chemistry", "Science");
            newSpecialty.Save();
            Tutor newTutor = new Tutor("Sean", "Miller", "sm@gmail.com", "1234567890", 1, true, "Weekends", 25.00);
            newTutor.Save();
            newSpecialty.AddTutor(newTutor);
            List<Tutor> expectedList = new List<Tutor> { newTutor };
            List<Tutor> actualList = newSpecialty.GetTutors();
            CollectionAssert.AreEqual(expectedList, actualList);
        } 
    }
}
