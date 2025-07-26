using Hospital.Models;

namespace Hospital.DTOs.ExaminationDtos;

public class CreateExaminationDto
{
    public PatientType PatientType { get; set; }
    public string HospitalName { get; set; }
    public string ClinicCode { get; set; }
    public string DoctorCode { get; set; }
    public string Statement { get; set; }
    public int PatientId { get; set; }
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }
    public List<CreateExaminationServiceDto> Services { get; set; } = new();
}