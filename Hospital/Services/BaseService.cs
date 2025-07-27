using Hospital.Models;
using Hospital.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using static Hospital.Utility.SD;

namespace Hospital.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpClientFactory { get; set; }
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            responseModel = new();
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(APIRequest request)
        {
            try
            {
                var client = httpClientFactory.CreateClient("HospitalAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.Url);
                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                }
                message.Method = request.ApiType switch
                {
                    ApiType.GET => HttpMethod.Get,
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };
                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                var ApiResponse = new APIResponse
                {
                    IsSuccess = apiResponse.IsSuccessStatusCode,
                    Result = apiContent, // The raw JSON string goes here
                    StatusCode = apiResponse.StatusCode

                };

                // Now, we serialize our response and deserialize it into the generic type T
                var res = JsonConvert.SerializeObject(ApiResponse);
                var finalResponse = JsonConvert.DeserializeObject<T>(res);
                return finalResponse;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse()
                {
                    IsSuccess = false,
                    ErrorMessage = [Convert.ToString(ex.Message)]
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponse = JsonConvert.DeserializeObject<T>(res);
                return apiResponse;
            }
        }
    }
}
