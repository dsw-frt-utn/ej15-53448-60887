using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

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
            string filePath = "specialities.json";

            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var specialitiesFromJson = JsonSerializer.Deserialize<List<Speciality>>(jsonString, options);

                if (specialitiesFromJson != null)
                {
                    Specialities = specialitiesFromJson;
                }
            }
        }

        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
        }

        public List<Doctor> GetActiveDoctors()
        {
            return Doctors.Where(d => d.IsActive).ToList();
        }

        public Doctor? GetActiveDoctorById(Guid id)
        {
            return Doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
        }

        public bool DeactivateDoctor(Guid id)
        {
            var doctor = GetActiveDoctorById(id);
            if (doctor != null)
            {
                doctor.IsActive = false;
                return true;
            }
            return false;
        }
    }
    }
