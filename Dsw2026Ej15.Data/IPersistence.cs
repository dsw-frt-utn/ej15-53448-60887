using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        List<Speciality> Specialities { get; }
        List<Doctor> Doctors { get; }

        void AddDoctor(Doctor doctor);
        List<Doctor> GetActiveDoctors();
        Doctor? GetActiveDoctorById(Guid id);
        bool DeactivateDoctor(Guid id);
    }
}
