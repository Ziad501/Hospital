using Hospital.Models;

namespace Hospital.DTOs.ExaminationDtos;

public class ExaminationDto
{
    public int Id { get; set; }
    public PatientType PatientType { get; set; }
    public string HospitalName { get; set; }
    public string ClinicCode { get; set; }
    public string ClinicName { get; set; }
    public string DoctorCode { get; set; }
    public string DoctorName { get; set; }
    public string Statement { get; set; }
    public DateTime ExaminationDate { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; }
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }
    public decimal NetAmount { get; set; }
    public bool IsPaid { get; set; }
    public List<ExaminationServiceDto> Services { get; set; } = new();
}