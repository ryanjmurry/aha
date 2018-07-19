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
    }
}
