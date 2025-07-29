using Hospital.DTOs;
using Hospital.DTOs.ExaminationDtos;
using Hospital.Models;
using Hospital.Services.IServices;
using Hospital.ViewModels;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Hospital.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHospitalServices _hospitalService;

        public HospitalController(IMapper mapper, IHospitalServices hospitalService)
        {
            _mapper = mapper;
            _hospitalService = hospitalService;
        }

        public async Task<IActionResult> CreateTicket()
        {
            var viewModel = new CreateTicketViewModel();
            await PopulateDropdowns(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicket(CreateTicketViewModel viewModel)
        {
            // First, check for server-side validation errors based on annotations
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, errors });
            }

            try
            {
                // Now, perform custom business logic validation for duplicate patient codes
                if (await _hospitalService.PatientCodeExistsAsync(viewModel.CreatePatient.Code))
                {
                    var errors = new[] { $"Patient code '{viewModel.CreatePatient.Code}' is already in use. Please enter a unique code." };
                    return Json(new { success = false, errors });
                }

                // Step 1: Save the Patient
                var patientEntity = _mapper.Map<Patient>(viewModel.CreatePatient);
                patientEntity.BirthDate = DateTime.SpecifyKind(viewModel.CreatePatient.BirthDate, DateTimeKind.Utc);
                var today = DateTime.UtcNow;
                var age = today.Year - patientEntity.BirthDate.Year;
                if (patientEntity.BirthDate.Date > today.AddYears(-age)) age--;
                patientEntity.Age = age;
                var addedPatient = await _hospitalService.AddAsync(patientEntity);

                // Step 2: Enrich and Save the Examination
                var examinationDto = viewModel.CreateExamination;
                var doctor = (await GetDoctors()).FirstOrDefault(d => d.Code == examinationDto.DoctorCode);
                var clinic = (await GetClinics()).FirstOrDefault(c => c.Code == examinationDto.ClinicCode);

                if (clinic == null || doctor == null)
                {
                    return Json(new { success = false, errors = new[] { "The selected clinic or doctor is invalid." } });
                }

                var examinationEntity = _mapper.Map<Examination>(examinationDto);
                examinationEntity.PatientId = addedPatient.Id;
                examinationEntity.Amount = examinationDto.Services.Sum(s => s.Price * s.Quantity);
                examinationEntity.NetAmount = examinationEntity.Amount - examinationEntity.Discount;
                var nameParts = new[] { addedPatient.FirstName, addedPatient.MiddleName, addedPatient.LastName };
                examinationEntity.PatientName = string.Join(" ", nameParts.Where(n => !string.IsNullOrWhiteSpace(n)));
                examinationEntity.DoctorName = doctor.Name;
                examinationEntity.ClinicName = clinic.Name;
                examinationEntity.ExaminationDate = DateTime.UtcNow;
                examinationEntity.IsPaid = true; // Mark as paid since a receipt is being generated
                examinationEntity.PriceBookCode = examinationDto.ClinicCode;
                var addedExamination = await _hospitalService.AddExaminationAsync(examinationEntity);

                // Step 3: Create and Save the Receipt
                var receiptEntity = new Receipt
                {
                    ExaminationId = addedExamination.Id,
                    ReceiptNumber = $"REC-{addedExamination.Id}-{DateTime.UtcNow:yyyyMMddHHss}",
                    IssueDate = DateTime.UtcNow,
                    TotalAmount = addedExamination.NetAmount,
                    PaidAmount = addedExamination.NetAmount,
                    PaymentMethod = PaymentMethod.Cash // Or another default
                };
                var addedReceipt = await _hospitalService.AddReceiptAsync(receiptEntity);

                // Step 4: Prepare DTO to return for PDF generation
                var receiptDto = _mapper.Map<ReceiptDto>(addedReceipt);
                receiptDto.Examination = _mapper.Map<ExaminationDto>(addedExamination);

                return Json(new { success = true, receipt = receiptDto });
            }
            catch (Exception ex)
            {
                // In a real app, you should log the exception (ex)
                return Json(new { success = false, errors = new[] { "An unexpected error occurred while saving." } });
            }
        }


        private async Task PopulateDropdowns(CreateTicketViewModel viewModel)
        {
            var doctors = await GetDoctors();
            var clinics = await GetClinics();
            viewModel.DoctorOptions = doctors.Select(d => new SelectListItem { Text = d.Name, Value = d.Code }).ToList();
            viewModel.ClinicOptions = clinics.Select(c => new SelectListItem { Text = c.Name, Value = c.Code }).ToList();
        }

        private async Task<List<DoctorDto>> GetDoctors()
        {
            var response = await _hospitalService.GetAllDoctorsAsync<APIResponse>();
            if (response != null && response.IsSuccess && response.Result != null)
            {
                var doctors = JsonConvert.DeserializeObject<List<DoctorDto>>(Convert.ToString(response.Result));
                return doctors?.Where(d => d != null && !string.IsNullOrWhiteSpace(d.Code) && !string.IsNullOrWhiteSpace(d.Name)).ToList() ?? new List<DoctorDto>();
            }
            return new List<DoctorDto>();
        }

        private async Task<List<ClinicDto>> GetClinics()
        {
            var response = await _hospitalService.GetAllClinicsAsync<APIResponse>();
            if (response != null && response.IsSuccess && response.Result != null)
            {
                var clinics = JsonConvert.DeserializeObject<List<ClinicDto>>(Convert.ToString(response.Result));
                return clinics?.Where(c => c != null && !string.IsNullOrWhiteSpace(c.Code) && !string.IsNullOrWhiteSpace(c.Name)).ToList() ?? new List<ClinicDto>();
            }
            return new List<ClinicDto>();
        }

        [HttpGet]
        public async Task<JsonResult> GetDoctorsByClinic(string clinicCode)
        {
            if (string.IsNullOrEmpty(clinicCode)) return Json(new List<object>());
            var allClinics = await GetClinics();
            var selectedClinic = allClinics.FirstOrDefault(c => c.Code == clinicCode);
            if (selectedClinic == null) return Json(new List<object>());
            var clinicAccountCode = selectedClinic.AccCode;
            var allDoctors = await GetDoctors();
            var filteredDoctors = allDoctors
                .Where(doctor => doctor.EadaAccCode == clinicAccountCode)
                .Select(doctor => new { doctor.Code, doctor.Name });
            return Json(filteredDoctors);
        }

        [HttpGet]
        public async Task<JsonResult> GetServices()
        {
            // Replace with your actual service call
            var services = new List<object>
            {
                new { Code = "SRV001", Name = "كشف عام", Price = 150.00m },
                new { Code = "SRV002", Name = "استشارة", Price = 250.00m },
                new { Code = "SRV003", Name = "تحليل دم", Price = 300.00m },
                new { Code = "SRV004", Name = "أشعة سينية", Price = 450.00m }
            };
            return Json(services);
        }
    }
}