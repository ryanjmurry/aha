using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TutorTime;

namespace TutorTime.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int TutorId { get; set; }
        public int ClientId { get; set; }
        public DateTime Time { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        

        public Appointment(int tutorId, int clientId, DateTime time, string streetAddress, string city, string state, string zip, int id = 0)
        {
            Id = id;
            TutorId = tutorId;
            ClientId = clientId;
            Time = time;
            StreetAddress = streetAddress;
            City = city;
            State = state;
            Zip = zip;
        }

        public override bool Equals(System.Object otherAppointment)
        {
            if (!(otherAppointment is Appointment))
            {
                return false;
            }
            else
            {
                Appointment newAppointment = (Appointment) otherAppointment;
                bool idEquality = (this.Id == newAppointment.Id);
                bool tutorIdEquality = (this.TutorId == newAppointment.TutorId);
                bool appointmentIdEquality = (this.ClientId == newAppointment.ClientId);
                bool timeEquality = (this.Time == newAppointment.Time);
                bool streetAddressEquality = (this.StreetAddress == newAppointment.StreetAddress);
                bool cityEquality = (this.City == newAppointment.City);
                bool stateEquality = (this.State == newAppointment.State);
                bool zipEquality = (this.Zip == newAppointment.Zip);
                return (idEquality && tutorIdEquality && appointmentIdEquality && timeEquality && streetAddressEquality && cityEquality && stateEquality && zipEquality);
            }
        }

        public static List<Appointment> GetAll()
        {
            List<Appointment> allAppointments = new List<Appointment> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM appointments;";
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
                allAppointments.Add(newAppointment);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allAppointments;
        }

        public static void DeleteAll()
        {
            List<Appointment> allAppointments = new List<Appointment> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM appointments;";
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
            cmd.CommandText = @"INSERT INTO appointments (tutor_id, client_id, time, street_address, city, state, zip) VALUES (@appointmentTutorId, @appointmentClientId, @appointmentTime, @appointmentStreetAddress, @appointmentCity, @appointmentState, @appointmentZip);";
            cmd.Parameters.AddWithValue("@appointmentTutorId", this.TutorId);
            cmd.Parameters.AddWithValue("@appointmentClientId", this.ClientId);
            cmd.Parameters.AddWithValue("@appointmentTime", this.Time);
            cmd.Parameters.AddWithValue("@appointmentStreetAddress", this.StreetAddress);
            cmd.Parameters.AddWithValue("@appointmentCity", this.City);
            cmd.Parameters.AddWithValue("@appointmentState", this.State);
            cmd.Parameters.AddWithValue("@appointmentZip", this.Zip);
            cmd.ExecuteNonQuery();

            Id = (int) cmd.LastInsertedId;

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public static Appointment Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM appointments WHERE id = @appointmentId;";
            cmd.Parameters.AddWithValue("@appointmentId", id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        
                int appointmentId = 0;
                int appointmentTutorId = 0;
                int appointmentClientId = 0;
                DateTime appointmentTime = new DateTime();
                string appointmentStreetAddress = "";
                string appointmentCity = "";
                string appointmentState = "";
                string appointmentZip = "";

            while(rdr.Read())
            {
                appointmentId = rdr.GetInt32(0);
                appointmentTutorId = rdr.GetInt32(1);
                appointmentClientId = rdr.GetInt32(2);
                appointmentTime = rdr.GetDateTime(3);
                appointmentStreetAddress = rdr.GetString(4);
                appointmentCity = rdr.GetString(5);
                appointmentState = rdr.GetString(6);
                appointmentZip = rdr.GetString(7);
            }

            Appointment foundAppointment = new Appointment (appointmentTutorId, appointmentClientId, appointmentTime, appointmentStreetAddress, appointmentCity, appointmentState, appointmentZip, appointmentId);
            conn.Close();
            if (conn != null)
            {
                conn.Close();
            }

            return foundAppointment;
        }

        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM appointments WHERE id = @appointmentId;";
            cmd.Parameters.AddWithValue("@appointmentId", id);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update(int tutorId, int clientId, DateTime time, string streetAddress, string city, string state, string zip, int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE appointments SET tutor_id = @appointmentTutorId, client_id = @appointmentClientId, time = @appointmentTime, street_address = @appointmentStreetAddress, city = @appointmentCity, state = @appointmentState, zip = @appointmentZip WHERE id = @appointmentId;";
            cmd.Parameters.AddWithValue("@appointmentTutorId", tutorId);
            cmd.Parameters.AddWithValue("@appointmentClientId", clientId);
            cmd.Parameters.AddWithValue("@appointmentTime", time);
            cmd.Parameters.AddWithValue("@appointmentStreetAddress", streetAddress);
            cmd.Parameters.AddWithValue("@appointmentCity", city);
            cmd.Parameters.AddWithValue("@appointmentState", state);
            cmd.Parameters.AddWithValue("@appointmentZip", zip);
            cmd.Parameters.AddWithValue("@appointmentId", id);
            cmd.ExecuteNonQuery();

            Id = id;
            TutorId = tutorId;
            ClientId = clientId;
            Time = time;
            StreetAddress = streetAddress;
            City = city;
            State = state;
            Zip = zip;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
