namespace Hospital.DTOs.ExaminationDtos;

public class ExaminationServiceDto
{
    public int Id { get; set; }
    public string ServiceCode { get; set; }
    public string ServiceName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}