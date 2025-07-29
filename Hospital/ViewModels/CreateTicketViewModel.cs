using Hospital.DTOs.ExaminationDtos;
using Hospital.DTOs.PatientDtos;
using Microsoft.AspNetCore.Mvc.Rendering; // Required for SelectListItem

namespace Hospital.ViewModels
{
    public class CreateTicketViewModel
    {
        public CreatePatientDto CreatePatient { get; set; } = new();
        public CreateExaminationDto CreateExamination { get; set; } = new();

        public IEnumerable<SelectListItem> DoctorOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ClinicOptions { get; set; } = new List<SelectListItem>();
    }
}