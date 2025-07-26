namespace Hospital.Services.IServices
{
    public interface IHospitalServices
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(int id);
        Task<T> AddAsync<T>(T entity);
        Task<T> UpdateAsync<T>(T entity);
        Task<bool> DeleteAsync<T>(int id);
    }
}
