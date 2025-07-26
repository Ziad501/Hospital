using Hospital.Models;

namespace Hospital.DTOs.PatientDtos;

public class PatientDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {MiddleName} {LastName}".Trim();
    public string Code { get; set; }
    public string Religion { get; set; }
    public string Nationality { get; set; }
    public string Mobile { get; set; }
    public string NationalId { get; set; }
    public string PassportAddress { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string ReferredFrom { get; set; }
    public string RefDoctorId { get; set; }
    public PatientType PatientType { get; set; }
}