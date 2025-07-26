using Hospital.DTOs.ExaminationDtos;
using Hospital.Models;

namespace Hospital.DTOs;

public class ReceiptDto
{
    public int Id { get; set; }
    public int ExaminationId { get; set; }
    public string ReceiptNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public ExaminationDto Examination { get; set; }
}