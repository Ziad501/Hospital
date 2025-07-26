namespace Hospital.DTOs.ExaminationDtos;

public class CreateExaminationServiceDto
{
    public string ServiceCode { get; set; }
    public string ServiceName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}