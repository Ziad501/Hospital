namespace Hospital.Models;

public class Doctor
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string EadaAccCode { get; set; }
    public string MName { get; set; }
    public string DocAccCode { get; set; }

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();
}