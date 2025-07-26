namespace Hospital.Models;

public class Clinic
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string AccCode { get; set; }

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();
}