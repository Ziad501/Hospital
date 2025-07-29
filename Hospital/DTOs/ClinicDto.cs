using Newtonsoft.Json;

namespace Hospital.DTOs;

public class ClinicDto
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("acc_code")]
    public string AccCode { get; set; }
}
