using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TutorTime;

namespace TutorTime.Models
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
            cmd.CommandText = @"SELECT * FROM specialties;";
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
    }
}

