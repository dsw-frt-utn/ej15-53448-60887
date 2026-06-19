using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        List<Doctor> Doctors { get; }
        List<Speciality> Specialities { get; }
        void AddDoctor(Doctor doctor);
    }
}
