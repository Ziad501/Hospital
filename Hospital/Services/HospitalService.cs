using Hospital.Models;
using Hospital.Persistence;
using Hospital.Services.IServices;
using Hospital.Utility;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Services;

public class HospitalService : BaseService, IHospitalServices
{
    private readonly AppDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private string BaseUrl1;
    private string BaseUrl2;
    public HospitalService(IHttpClientFactory httpClientFactory, IConfiguration configuration, AppDbContext context) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _context = context;
        BaseUrl1 = configuration.GetValue<string>("ServiceUrls:Doctors");
        BaseUrl2 = configuration.GetValue<string>("ServiceUrls:Clinics");
    }

    public Task<T> GetAllDoctorsAsync<T>()
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = BaseUrl1
        });
    }

    public Task<T> GetDoctorByIdAsync<T>(int id)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{BaseUrl1}/{id}"
        });
    }
    public Task<T> GetAllClinicsAsync<T>()
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = BaseUrl2
        });
    }

    public Task<T> GetClinicByIdAsync<T>(int id)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{BaseUrl2}/{id}"
        });
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<Examination> AddExaminationAsync(Examination examination)
    {
        // Your logic to add the examination to the database and save changes
        await _context.Set<Examination>().AddAsync(examination);
        await _context.SaveChangesAsync();
        return examination;
    }
    public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> DeleteAsync<TEntity>(int id) where TEntity : class
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        return entity;
    }
    public async Task<bool> PatientCodeExistsAsync(string code)
    {
        return await _context.Patients.AnyAsync(p => p.Code == code);
    }
    public async Task<Receipt> AddReceiptAsync(Receipt receipt)
    {
        await _context.Receipts.AddAsync(receipt);

        await _context.SaveChangesAsync();

        return receipt;
    }
}
