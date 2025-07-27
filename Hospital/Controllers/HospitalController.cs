using Hospital.DTOs;
using Hospital.DTOs.PatientDtos;
using Hospital.Models;
using Hospital.Services.IServices;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hospital.Controllers;

public class HospitalController(IMapper mapper, IHospitalServices hospitalService) : Controller
{
    public async Task<IActionResult> Doctors()
    {
        List<Doctor> doctors = new();
        var response = await hospitalService.GetAllDoctorsAsync<APIResponse>();
        if (response != null && response.IsSuccess)
        {
            doctors = JsonConvert.DeserializeObject<List<Doctor>>(Convert.ToString(response.Result));
        }
        var doctorList = mapper.Map<List<DoctorDto>>(doctors);
        return View(doctorList);
    }
    public async Task<IActionResult> Clinic()
    {
        List<Clinic> clinics = new();
        var response = await hospitalService.GetAllClinicsAsync<APIResponse>();
        if (response != null && response.IsSuccess)
        {
            clinics = JsonConvert.DeserializeObject<List<Clinic>>(Convert.ToString(response.Result));
        }
        var clinicList = mapper.Map<List<ClinicDto>>(clinics);
        return View(clinicList);
    }
    public async Task<IActionResult> CreateTicket() { return View(); }
    [HttpPost]
    public async Task<IActionResult> CreateTicket(CreatePatientDto dto)
    {
        if (ModelState.IsValid)
        {
            dto.BirthDate = DateTime.SpecifyKind(dto.BirthDate, DateTimeKind.Utc);

            var patientEntity = mapper.Map<Patient>(dto);

            // Calculate age based on UTC for consistency.
            var today = DateTime.UtcNow;
            var age = today.Year - dto.BirthDate.Year;
            if (dto.BirthDate.Date > today.AddYears(-age))
            {
                age--;
            }
            patientEntity.Age = age;

            var addedPatient = await hospitalService.AddAsync(patientEntity);
            if (addedPatient != null)
            {
                return RedirectToAction(nameof(Doctors));
            }
        }
        return View(dto);
    }
}
