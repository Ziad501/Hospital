using Newtonsoft.Json;

namespace Hospital.DTOs;

public class DoctorDto
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("eada_acc_code")]
    public string EadaAccCode { get; set; }

    [JsonProperty("m_name")]
    public string MName { get; set; }

    [JsonProperty("doc_acc_code")]
    public string DocAccCode { get; set; }
}