using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TutorTime;
using TutorTime.Models;

namespace TutorTime.ViewModels
{
    public class ViewModel
    {
        public List<Tutor> AllTutors { get; set; }
        public List<Client> AllClients { get; set; }
        public List<Appointment> AllAppointments { get; set; }
        public List<Specialty> AllSpecialties { get; set; }
        public Tutor CurrentTutor { get; set; }
        public Client CurrentClient { get; set; }
        public Appointment CurrentAppointment { get; set; }
        public Specialty CurrentSpecialty { get; set; }

        public ViewModel()
        {
            AllTutors = Tutor.GetAll();
            AllClients = Client.GetAll();
            AllAppointments = Appointment.GetAll();
            AllSpecialties = Specialty.GetAll();
        }

        public void FindTutor(int id)
        {
            CurrentTutor = Tutor.Find(id);
        }

        public void FindClient(int id)
        {
            CurrentClient = Client.Find(id);
        }

        public void FindAppointment(int id)
        {
            CurrentAppointment = Appointment.Find(id);
        }

        public void FindSpecialty(int id)
        {
            CurrentSpecialty = Specialty.Find(id);
        }
    }
}