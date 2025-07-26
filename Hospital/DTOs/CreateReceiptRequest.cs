using Hospital.Models;

namespace Hospital.DTOs;

public class CreateReceiptRequest
{
    public PaymentMethod PaymentMethod { get; set; }
    public decimal PaidAmount { get; set; }
}