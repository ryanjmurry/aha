using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TutorTime.Models;
using TutorTime;

namespace TutorTime.Tests
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
            DateTime birthday = new DateTime (1111, 11, 11);
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday, 1);
            Assert.AreEqual("Ashley", newClient.FirstName);
            Assert.AreEqual("Adelman", newClient.LastName);
            Assert.AreEqual("aa@gmail.com", newClient.Email);
            Assert.AreEqual("1234567890", newClient.PhoneNumber);
            Assert.AreEqual("123 ABC Street", newClient.StreetAddress);
            Assert.AreEqual("Xyz", newClient.City);
            Assert.AreEqual("ZZ", newClient.State);
            Assert.AreEqual("12345", newClient.Zip);
            Assert.AreEqual(birthday, newClient.Birthday);
            Assert.AreEqual(1, newClient.Id);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfPropertiesAreSame_True()
        {
            DateTime birthday = new DateTime (1111, 11, 11);
            Client newClient1 = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday, 1);
            Client newClient2 = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday, 1);
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
            DateTime birthday = new DateTime (1111, 11, 11);
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday);
            newClient.Save();
            List<Client> expectedList = new List<Client> { newClient };
            List<Client> actualList = Client.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void DeleteAll_DeletesAllClientsFromDb_ClientList()
        {
            DateTime birthday = new DateTime (1111, 11, 11);
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday);
            newClient.Save();
            Client.DeleteAll();
            List<Client> expectedList = new List<Client> { };
            List<Client> actualList = Client.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Find_FindsClientInDb_Client()
        {
            DateTime birthday = new DateTime (1111, 11, 11);
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday);
            newClient.Save();
            Client foundClient = Client.Find(newClient.Id);
            Assert.AreEqual(newClient, foundClient);
        }

        [TestMethod]
        public void Delete_DeletesClientInDb_ClientList()
        {
            DateTime birthday = new DateTime (1111, 11, 11);
            Client newClient1 = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday);
            newClient1.Save();
            Client newClient2 = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday);
            newClient2.Save();
            Client.Delete(newClient1.Id);
            List<Client> expectedList = new List<Client> { newClient2 };
            List<Client> actualList = Client.GetAll();
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void Update_UpdateClientFromDb_String()
        {
            DateTime birthday = new DateTime (1111, 11, 11);
            Client newClient = new Client("Ashley", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday, 1);
            newClient.Save();
            newClient.Update("Bill", "Adelman", "aa@gmail.com", "1234567890", "123 ABC Street", "Xyz", "ZZ", "12345", birthday, newClient.Id);
            string actualFirstName = Client.Find(newClient.Id).FirstName;
            Assert.AreEqual("Bill", actualFirstName);
        }
    }
}
