using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Aha;

namespace Aha.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Discipline { get; set; }

        public Specialty(string subject, string discipline, int id = 0)
        {
            Id = id;
            Subject = subject;
            Discipline = discipline;
        }

        public override bool Equals(System.Object otherSpecialty)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                Specialty newSpecialty = (Specialty) otherSpecialty;
                bool idEquality = (this.Id == newSpecialty.Id);
                bool subjectEquality = (this.Subject == newSpecialty.Subject);
                bool disciplinequality = (this.Discipline == newSpecialty.Discipline);
                return (idEquality && subjectEquality && disciplinequality);
            }
        }

        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialties = new List<Specialty> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties ORDER BY subject;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int specialtyId = rdr.GetInt32(0);
                string specialtySubject = rdr.GetString(1);
                string specialtyDiscipline = rdr.GetString(2);
                Specialty newSpecialty = new Specialty (specialtySubject, specialtyDiscipline, specialtyId);
                allSpecialties.Add(newSpecialty);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }

        public static void DeleteAll()
        {
            List<Specialty> allSpecialties = new List<Specialty> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties;";
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
            cmd.CommandText = @"INSERT INTO specialties (subject, discipline) VALUES (@specialtySubject, @specialtyDiscipline);";
            cmd.Parameters.AddWithValue("@specialtySubject", this.Subject);
            cmd.Parameters.AddWithValue("@specialtyDiscipline", this.Discipline);
            cmd.ExecuteNonQuery();

            Id = (int) cmd.LastInsertedId;

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public static Specialty Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id = @specialtyId;";
            cmd.Parameters.AddWithValue("@specialtyId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        
                int specialtyId = 0;
                string specialtySubject = "";
                string specialtyDiscipline = "";

            while(rdr.Read())
            {
                specialtyId = rdr.GetInt32(0);
                specialtySubject = rdr.GetString(1);
                specialtyDiscipline = rdr.GetString(2);
            }

            Specialty foundSpecialty = new Specialty (specialtySubject, specialtyDiscipline, specialtyId);
            conn.Close();
            if (conn != null)
            {
                conn.Close();
            }

            return foundSpecialty;
        }

        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties WHERE id = @specialtyId;";
            cmd.Parameters.AddWithValue("@specialtyId", id);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update(string subject, string discipline, int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE specialties SET subject = @specialtySubject, discipline = @specialtyDiscipline WHERE id = @specialtyId;";
            cmd.Parameters.AddWithValue("@specialtySubject", subject);
            cmd.Parameters.AddWithValue("@specialtyDiscipline", discipline);
            cmd.Parameters.AddWithValue("@specialtyId", id);
            cmd.ExecuteNonQuery();

            Id = id;
            Subject = subject;
            Discipline = discipline;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddClient(Client newClient)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients_needs (client_id, specialty_id) VALUES (@clientId, @specialtyId);";
            cmd.Parameters.AddWithValue("@specialtyID", this.Id);
            cmd.Parameters.AddWithValue("@clientId", newClient.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Client> GetClients()
        {
            List<Client> allSpecialtyClients = new List<Client> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT clients.* FROM specialties
                JOIN clients_needs ON (specialties.id = clients_needs.specialty_id)
                JOIN clients ON (clients_needs.client_id = clients.id)
                WHERE specialties.id = @specialtyId;";
            cmd.Parameters.AddWithValue("@specialtyId", this.Id);
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
                DateTime clientBirthday = rdr.GetDateTime(9);
                Client newClient = new Client (clientFirstName, clientLastName, clientEmail, clientPhoneNumber, clientStreetAddress, clientCity, clientState, clientZip, clientBirthday, clientId);
                allSpecialtyClients.Add(newClient);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialtyClients;
        }

        public void AddTutor(Tutor newTutor)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tutors_specialties (tutor_id, specialty_id) VALUES (@tutorId, @specialtyId);";
            cmd.Parameters.AddWithValue("@tutorId", newTutor.Id);
            cmd.Parameters.AddWithValue("@specialtyId", this.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Tutor> GetTutors()
        {
            List<Tutor> allSpecialtyTutors = new List<Tutor> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT tutors.* FROM specialties
                JOIN tutors_specialties ON (specialties.id = tutors_specialties.specialty_id)
                JOIN tutors ON (tutors_specialties.tutor_id = tutors.id)
                WHERE specialties.id = @specialtyId;";
            cmd.Parameters.AddWithValue("specialtyId", this.Id);
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
                allSpecialtyTutors.Add(newTutor);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialtyTutors;
        }
    }
}

