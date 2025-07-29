using Hospital.Models;

namespace Hospital.Services.IServices
{
    public interface IHospitalServices
    {
        Task<T> GetAllDoctorsAsync<T>();
        Task<T> GetDoctorByIdAsync<T>(int id);
        Task<T> GetAllClinicsAsync<T>();
        Task<T> GetClinicByIdAsync<T>(int id);
        Task<Examination> AddExaminationAsync(Examination examination);
        Task<bool> PatientCodeExistsAsync(string code);
        Task<Receipt> AddReceiptAsync(Receipt receipt);
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class;
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity> DeleteAsync<TEntity>(int id) where TEntity : class;

    }
}
