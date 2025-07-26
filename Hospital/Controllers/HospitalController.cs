using Hospital.Models;
using Hospital.Services.IServices;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hospital.Controllers;

public class HospitalController(IMapper mapper, IHospitalServices hospitalService) : Controller
{
    public async Task<IActionResult> IndexDoc()
    {
        List<Doctor> doctors = new();
        var response = await hospitalService.GetAllAsync<APIResponse>();
        if (response != null && response.IsSuccess)
        {
            doctors = JsonConvert.DeserializeObject<List<Doctor>>(Convert.ToString(response.Result));
        }
        return View(doctors);
    }
}
