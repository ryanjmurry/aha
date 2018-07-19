using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TutorTime;

namespace TutorTime.Models
{
    public class Tutor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Experience { get; set; }
        public bool Credential { get; set; }
        public string Availability { get; set; }
        public double Rate { get; set; }

        public Tutor(string firstName, string lastName, string email, string phoneNumber, int experience, bool credential, string availability, double rate, int id = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Experience = experience;
            Credential = credential;
            Availability = availability;
            Rate = rate;
        }

        public override bool Equals(System.Object otherTutor)
        {
            if (!(otherTutor is Tutor))
            {
                return false;
            }
            else
            {
                Tutor newTutor = (Tutor) otherTutor;
                bool idEquality = (this.Id == newTutor.Id);
                bool firstNameEquality = (this.FirstName == newTutor.FirstName);
                bool lastNameEquality = (this.LastName == newTutor.LastName);
                bool emailEquality = (this.Email == newTutor.Email);
                bool phoneNumberEquality = (this.PhoneNumber == newTutor.PhoneNumber);
                bool experienceEquality = (this.Experience == newTutor.Experience);
                bool credentialEquality = (this.Credential == newTutor.Credential);
                bool availabilityEquality = (this.Availability == newTutor.Availability);
                bool rateEquality = (this.Rate == newTutor.Rate);
                return (idEquality && firstNameEquality && lastNameEquality && emailEquality && phoneNumberEquality && experienceEquality && credentialEquality && availabilityEquality && rateEquality);
            }
        }

        public static List<Tutor> GetAll()
        {
            List<Tutor> allTutors = new List<Tutor> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tutors;";
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
                allTutors.Add(newTutor);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allTutors;
        }

        public static void DeleteAll()
        {
            List<Tutor> allTutors = new List<Tutor> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tutors;";
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
            cmd.CommandText = @"INSERT INTO tutors (first_name, last_name, email, phone_number, experience, credential, availability, rate) VALUES (@tutorFirstName, @tutorLastName, @tutorEmail, @tutorPhoneNumber, @tutorExperience, @tutorCredential, @tutorAvailability, @tutorRate);";
            cmd.Parameters.AddWithValue("@tutorFirstName", this.FirstName);
            cmd.Parameters.AddWithValue("@tutorLastName", this.LastName);
            cmd.Parameters.AddWithValue("@tutorEmail", this.Email);
            cmd.Parameters.AddWithValue("@tutorPhoneNumber", this.PhoneNumber);
            cmd.Parameters.AddWithValue("@tutorExperience", this.Experience);
            cmd.Parameters.AddWithValue("@tutorCredential", this.Credential);
            cmd.Parameters.AddWithValue("@tutorAvailability", this.Availability);
            cmd.Parameters.AddWithValue("@tutorRate", this.Rate);
            cmd.ExecuteNonQuery();

            Id = (int) cmd.LastInsertedId;

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public static Tutor Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tutors WHERE id = @tutorId;";
            cmd.Parameters.AddWithValue("@tutorId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        
                int tutorId = 0;
                string tutorFirstName = "";
                string tutorLastName = "";
                string tutorEmail = "";
                string tutorPhoneNumber = "";
                int tutorExperience = 0;
                bool tutorCredential = true;
                string tutorAvailability = "";
                double tutorRate = 0.00;

            while(rdr.Read())
            {
                tutorId = rdr.GetInt32(0);
                tutorFirstName = rdr.GetString(1);
                tutorLastName = rdr.GetString(2);
                tutorEmail = rdr.GetString(3);
                tutorPhoneNumber = rdr.GetString(4);
                tutorExperience = rdr.GetInt32(5);
                tutorCredential = rdr.GetBoolean(6);
                tutorAvailability = rdr.GetString(7);
                tutorRate = rdr.GetDouble(8);
            }

            Tutor foundTutor = new Tutor (tutorFirstName, tutorLastName, tutorEmail, tutorPhoneNumber, tutorExperience, tutorCredential, tutorAvailability, tutorRate, tutorId);
            conn.Close();
            if (conn != null)
            {
                conn.Close();
            }

            return foundTutor;
        }

        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tutors WHERE id = @tutorId;";
            cmd.Parameters.AddWithValue("@tutorId", id);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update(string firstName, string lastName, string email, string phoneNumber, int experience, bool credential, string availability, double rate, int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tutors SET first_name = @tutorFirstName, last_name = @tutorLastName, email = @tutorEmail, phone_number = @tutorPhoneNumber, experience = @tutorExperience, credential = @tutorCredential, availability = @tutorAvailability, rate = @tutorRate WHERE id = @tutorId;";
            cmd.Parameters.AddWithValue("@tutorFirstName", firstName);
            cmd.Parameters.AddWithValue("@tutorLastName", lastName);
            cmd.Parameters.AddWithValue("@tutorEmail", email);
            cmd.Parameters.AddWithValue("@tutorPhoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@tutorExperience", experience);
            cmd.Parameters.AddWithValue("@tutorCredential", credential);
            cmd.Parameters.AddWithValue("@tutorAvailability", availability);
            cmd.Parameters.AddWithValue("@tutorRate", rate);
            cmd.Parameters.AddWithValue("@tutorId", id);
            cmd.ExecuteNonQuery();

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Experience = experience;
            Credential = credential;
            Availability = availability;
            Rate = rate;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
