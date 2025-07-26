namespace Hospital.Models;

public class ExaminationService
{
    public int Id { get; set; }
    public int ExaminationId { get; set; }
    public string ServiceCode { get; set; }
    public string ServiceName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }

    public virtual Examination Examination { get; set; }
}