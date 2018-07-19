using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TutorTime.Models;
using TutorTime;

namespace TutorTime.Tests
{
    [TestClass]
    public class TutorTest
    {
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
    }
}
