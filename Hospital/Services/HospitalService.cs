using Hospital.Models;
using Hospital.Services.IServices;
using Hospital.Utility;

namespace Hospital.Services
{
    public class HospitalService : BaseService, IHospitalServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string BaseUrl;
        public HospitalService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            BaseUrl = configuration.GetValue<string>("ServiceUrls:Doctors");
        }
        public Task<T> AddAsync<T>(T entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = entity,
                Url = BaseUrl
            });
        }

        public Task<bool> DeleteAsync<T>(int id)
        {
            return SendAsync<bool>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{BaseUrl}/{id}"
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = BaseUrl
            });
        }

        public Task<T> GetByIdAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{BaseUrl}/{id}"
            });
        }

        public Task<T> UpdateAsync<T>(T entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = entity,
                Url = BaseUrl
            });
        }
    }
}
