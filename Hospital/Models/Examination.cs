namespace Hospital.Models;

public class Examination
{
    public int Id { get; set; }
    public PatientType PatientType { get; set; }
    public string? HospitalName { get; set; }
    public string? ClinicCode { get; set; }
    public string? PriceBookCode { get; set; }
    public ContractType ContractType { get; set; }
    public string? DoctorCode { get; set; }
    public string? Statement { get; set; }
    public DateTime ExaminationDate { get; set; }
    public int PatientId { get; set; }
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }
    public decimal NetAmount { get; set; }
    public bool IsPaid { get; set; }
    public string? ClinicName { get; set; }
    public string? DoctorName { get; set; }
    public string? PatientName { get; set; }
    public virtual Patient Patient { get; set; }
    public virtual ICollection<ExaminationService> Services { get; set; } = new List<ExaminationService>();
}