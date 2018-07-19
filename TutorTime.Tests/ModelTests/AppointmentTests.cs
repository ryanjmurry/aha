using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TutorTime.Models;

namespace TutorTime.Tests
{
    [TestClass]
    public class AppointmentTest : IDisposable
    {
        public void Dispose()
        {
            Appointment.DeleteAll();
        }

        public AppointmentTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=rjm_test;";
        }

        [TestMethod]
        public void GetSet_GetsAndSetsAppointmentProperties_Properties()
        {
            DateTime time = new DateTime (1111, 11, 11);
            Appointment newAppointment = new Appointment(1, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345", 1);
            Assert.AreEqual(1, newAppointment.TutorId);
            Assert.AreEqual(1, newAppointment.ClientId);
            Assert.AreEqual(time, newAppointment.Time);
            Assert.AreEqual("123 ABC Street", newAppointment.StreetAddress);
            Assert.AreEqual("Xyz", newAppointment.City);
            Assert.AreEqual("ZZ", newAppointment.State);
            Assert.AreEqual("12345", newAppointment.Zip);
            Assert.AreEqual(time, newAppointment.Time);
            Assert.AreEqual(1, newAppointment.Id);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfPropertiesAreSame_True()
        {
            DateTime time = new DateTime (1111, 11, 11);
            Appointment newAppointment1 = new Appointment(1, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345", 1);
            Appointment newAppointment2 = new Appointment(1, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345", 1);
            Assert.AreEqual(newAppointment1, newAppointment2);
        }

        [TestMethod]
        public void GetAll_ChecksDbStartsEmpty_0()
        {
            int result = Appointment.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesAppointmentToDb_AppointmentList()
        {
            DateTime time = new DateTime (1111, 11, 11);
            Appointment newAppointment = new Appointment(1, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345");
            newAppointment.Save();
            List<Appointment> expectedList = new List<Appointment> { newAppointment };
            List<Appointment> actualList = Appointment.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void DeleteAll_DeletesAllAppointmentsFromDb_AppointmentList()
        {
            DateTime time = new DateTime (1111, 11, 11);
            Appointment newAppointment = new Appointment(1, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345");
            newAppointment.Save();
            Appointment.DeleteAll();
            List<Appointment> expectedList = new List<Appointment> { };
            List<Appointment> actualList = Appointment.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Find_FindsAppointmentInDb_Appointment()
        {
            DateTime time = new DateTime (1111, 11, 11);
            Appointment newAppointment = new Appointment(1, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345");
            newAppointment.Save();
            Appointment foundAppointment = Appointment.Find(newAppointment.Id);
            Assert.AreEqual(newAppointment, foundAppointment);
        }

        [TestMethod]
        public void Delete_DeletesAppointmentInDb_AppointmentList()
        {
            DateTime time = new DateTime (1111, 11, 11);
            Appointment newAppointment1 = new Appointment(1, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345");
            newAppointment1.Save();
            Appointment newAppointment2 = new Appointment(1, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345");
            newAppointment2.Save();
            Appointment.Delete(newAppointment1.Id);
            List<Appointment> expectedList = new List<Appointment> { newAppointment2 };
            List<Appointment> actualList = Appointment.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Update_UpdateAppointmentFromDb_Int()
        {
            DateTime time = new DateTime (1111, 11, 11);
            Appointment newAppointment = new Appointment(1, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345");
            newAppointment.Save();
            newAppointment.Update(2, 1, time, "123 ABC Street", "Xyz", "ZZ", "12345", newAppointment.Id);
            int actualTutorId = Appointment.Find(newAppointment.Id).TutorId;
            Assert.AreEqual(2, actualTutorId);
        }
    }
}
