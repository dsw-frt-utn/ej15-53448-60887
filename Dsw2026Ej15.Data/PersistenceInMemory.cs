using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        public List<Doctor> Doctors { get; private set; } = new();
        public List<Speciality> Specialities { get; private set; } = new();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
           
        }

        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
        }
    }
}
