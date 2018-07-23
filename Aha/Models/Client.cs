using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Aha;

namespace Aha.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int Age { get; set; }

        public Client(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string city, string state, string zip, int age, int id = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            StreetAddress = streetAddress;
            City = city;
            State = state;
            Zip = zip;
            Age = age;
        }

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = (this.Id == newClient.Id);
                bool firstNameEquality = (this.FirstName == newClient.FirstName);
                bool lastNameEquality = (this.LastName == newClient.LastName);
                bool emailEquality = (this.Email == newClient.Email);
                bool phoneNumberEquality = (this.PhoneNumber == newClient.PhoneNumber);
                bool streetAddressEquality = (this.StreetAddress == newClient.StreetAddress);
                bool cityEquality = (this.City == newClient.City);
                bool stateEquality = (this.State == newClient.State);
                bool zipEquality = (this.Zip == newClient.Zip);
                bool ageEquality = (this.Age == newClient.Age);
                return (idEquality && firstNameEquality && lastNameEquality && emailEquality && phoneNumberEquality && streetAddressEquality && cityEquality && stateEquality && zipEquality && ageEquality);
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientFirstName = rdr.GetString(1);
                string clientLastName = rdr.GetString(2);
                string clientEmail = rdr.GetString(3);
                string clientPhoneNumber = rdr.GetString(4);
                string clientStreetAddress = rdr.GetString(5);
                string clientCity = rdr.GetString(6);
                string clientState = rdr.GetString(7);
                string clientZip = rdr.GetString(8);
                int clientAge = rdr.GetInt32(9);
                Client newClient = new Client (clientFirstName, clientLastName, clientEmail, clientPhoneNumber, clientStreetAddress, clientCity, clientState, clientZip, clientAge, clientId);
                allClients.Add(newClient);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static void DeleteAll()
        {
            List<Client> allClients = new List<Client> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (first_name, last_name, email, phone_number, street_address, city, state, zip, age) VALUES (@clientFirstName, @clientLastName, @clientEmail, @clientPhoneNumber, @clientStreetAddress, @clientCity, @clientState, @clientZip, @clientAge);";
            cmd.Parameters.AddWithValue("@clientFirstName", this.FirstName);
            cmd.Parameters.AddWithValue("@clientLastName", this.LastName);
            cmd.Parameters.AddWithValue("@clientEmail", this.Email);
            cmd.Parameters.AddWithValue("@clientPhoneNumber", this.PhoneNumber);
            cmd.Parameters.AddWithValue("@clientStreetAddress", this.StreetAddress);
            cmd.Parameters.AddWithValue("@clientCity", this.City);
            cmd.Parameters.AddWithValue("@clientState", this.State);
            cmd.Parameters.AddWithValue("@clientZip", this.Zip);
            cmd.Parameters.AddWithValue("@clientAge", this.Age);
            cmd.ExecuteNonQuery();

            Id = (int) cmd.LastInsertedId;

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = @clientId;";
            cmd.Parameters.AddWithValue("@clientId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        
                int clientId = 0;
                string clientFirstName = "";
                string clientLastName = "";
                string clientEmail = "";
                string clientPhoneNumber = "";
                string clientStreetAddress = "";
                string clientCity = "";
                string clientState = "";
                string clientZip = "";
                int clientAge = 0;

            while(rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                clientFirstName = rdr.GetString(1);
                clientLastName = rdr.GetString(2);
                clientEmail = rdr.GetString(3);
                clientPhoneNumber = rdr.GetString(4);
                clientStreetAddress = rdr.GetString(5);
                clientCity = rdr.GetString(6);
                clientState = rdr.GetString(7);
                clientZip = rdr.GetString(8);
                clientAge = rdr.GetInt32(9);
            }

            Client foundClient = new Client (clientFirstName, clientLastName, clientEmail, clientPhoneNumber, clientStreetAddress, clientCity, clientState, clientZip, clientAge, clientId);
            conn.Close();
            if (conn != null)
            {
                conn.Close();
            }

            return foundClient;
        }

        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id = @clientId;";
            cmd.Parameters.AddWithValue("@clientId", id);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string city, string state, string zip, int age, int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET first_name = @clientFirstName, last_name = @clientLastName, email = @clientEmail, phone_number = @clientPhoneNumber, street_address = @clientStreetAddress, city = @clientCity, state = @clientState, zip = @clientZip, age = @clientAge WHERE id = @clientId;";
            cmd.Parameters.AddWithValue("@clientFirstName", firstName);
            cmd.Parameters.AddWithValue("@clientLastName", lastName);
            cmd.Parameters.AddWithValue("@clientEmail", email);
            cmd.Parameters.AddWithValue("@clientPhoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@clientStreetAddress", streetAddress);
            cmd.Parameters.AddWithValue("@clientCity", city);
            cmd.Parameters.AddWithValue("@clientState", state);
            cmd.Parameters.AddWithValue("@clientZip", zip);
            cmd.Parameters.AddWithValue("@clientAge", age);
            cmd.Parameters.AddWithValue("@clientId", id);
            cmd.ExecuteNonQuery();

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            StreetAddress = streetAddress;
            City = city;
            State = state;
            Zip = zip;
            Age = age;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddTutor(Tutor newTutor)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tutors_clients (tutor_id, client_id) VALUES (@tutorId, @clientId);";
            cmd.Parameters.AddWithValue("@tutorId", newTutor.Id);
            cmd.Parameters.AddWithValue("@clientId", this.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void DeleteTutor(Tutor newTutor)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tutors_clients WHERE tutor_id = @tutorId AND client_id = @clientId;";
            cmd.Parameters.AddWithValue("@tutorId", newTutor.Id);
            cmd.Parameters.AddWithValue("@clientId", this.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Tutor> GetTutors()
        {
            List<Tutor> allClientTutors = new List<Tutor> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT tutors.* FROM clients
                JOIN tutors_clients ON (clients.id = tutors_clients.client_id)
                JOIN tutors ON (tutors_clients.tutor_id = tutors.id)
                WHERE clients.id = @clientId;";
            cmd.Parameters.AddWithValue("clientId", this.Id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int tutorId = rdr.GetInt32(0);
                string tutorFirstName = rdr.GetString(1);
                string tutorLastName = rdr.GetString(2);
                string tutorEmail = rdr.GetString(3);
                string tutorPhoneNumber = rdr.GetString(4);
                int tutorExperience = rdr.GetInt32(5);
                bool tutorCredential = rdr.GetBoolean(6);
                string tutorAvailability = rdr.GetString(7);
                double tutorRate = rdr.GetDouble(8);
                Tutor newTutor = new Tutor (tutorFirstName, tutorLastName, tutorEmail, tutorPhoneNumber, tutorExperience, tutorCredential, tutorAvailability, tutorRate, tutorId);
                allClientTutors.Add(newTutor);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClientTutors;
        }

        public List<Appointment> GetAppointments()
        {
            List<Appointment> allClientAppointments = new List<Appointment> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM appointments WHERE client_id = @clientId;";
            cmd.Parameters.AddWithValue("@clientId", this.Id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int appointmentId = rdr.GetInt32(0);
                int appointmentTutorId = rdr.GetInt32(1);
                int appointmentClientId = rdr.GetInt32(2);
                DateTime appointmentTime = rdr.GetDateTime(3);
                string appointmentStreetAddress = rdr.GetString(4);
                string appointmentCity = rdr.GetString(5);
                string appointmentState = rdr.GetString(6);
                string appointmentZip = rdr.GetString(7);
                Appointment newAppointment = new Appointment (appointmentTutorId, appointmentClientId, appointmentTime, appointmentStreetAddress, appointmentCity, appointmentState, appointmentZip, appointmentId);
                allClientAppointments.Add(newAppointment);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClientAppointments;
        }

        public void AddNeed(Specialty newNeed)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients_needs (client_id, specialty_id) VALUES (@clientId, @specialtyId);";
            cmd.Parameters.AddWithValue("@clientId", this.Id);
            cmd.Parameters.AddWithValue("@specialtyId", newNeed.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void DeleteNeed(Specialty deleteNeed)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients_needs WHERE client_id = @clientId AND specialty_id = @needId;";
            cmd.Parameters.AddWithValue("@clientId", this.Id);
            cmd.Parameters.AddWithValue("@needId", deleteNeed.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Specialty> GetNeeds()
        {
            List<Specialty> allClientNeeds = new List<Specialty> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM clients
                JOIN clients_needs ON (clients.id = clients_needs.client_id)
                JOIN specialties ON (clients_needs.specialty_id = specialties.id)
                WHERE clients.id = @clientId;";
            cmd.Parameters.AddWithValue("@clientId", this.Id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int needId = rdr.GetInt32(0);
                string needSubject = rdr.GetString(1);
                string needDiscipline = rdr.GetString(2);
                Specialty newNeed = new Specialty (needSubject, needDiscipline, needId);
                allClientNeeds.Add(newNeed);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClientNeeds;
        }
    }
}
