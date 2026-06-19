using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public IActionResult Create([FromBody] DoctorRequest request)
    {
        if (string.IsNullOrEmpty(request.Name)) throw new ValidationException("El nombre es requerido");
        if (string.IsNullOrEmpty(request.LicenseNumber)) throw new ValidationException("El número de licencia es requerido");

        var speciality = _persistence.Specialities.FirstOrDefault(s => s.Id == request.SpecialityId);
        if (speciality == null) throw new ValidationException("La especialidad no existe");

        var newDoctor = new Doctor
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            Speciality = speciality,
            IsActive = true
        };

        _persistence.AddDoctor(newDoctor);
        return StatusCode(201, newDoctor);
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_persistence.GetActiveDoctors());

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var doctor = _persistence.GetActiveDoctorById(id);
        if (doctor == null) return NotFound("Médico no encontrado o inactivo");
        return Ok(doctor);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (!_persistence.DeactivateDoctor(id)) return NotFound("Médico no encontrado");
        return NoContent();
    }
}

public record DoctorRequest(string Name, string LicenseNumber, Guid SpecialityId);