namespace Hospital.Models;

public class Receipt
{
    public int Id { get; set; }
    public int ExaminationId { get; set; }
    public string ReceiptNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public virtual Examination Examination { get; set; }
}