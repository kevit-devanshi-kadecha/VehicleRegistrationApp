using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using VehicleRegistrationWebApp.Models;

namespace VehicleRegistrationWebApp.Services
{
    public class VehicleService 
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public VehicleService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<VehicleViewModel>> GetVehicles(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                throw new ArgumentNullException(nameof(jwtToken), "JWT token cannot be null or empty.");
            }
            try
            {
                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(_configuration["ApiBaseAddress"] + "api/Vehicle/getAllVehicles"),
                        Method = HttpMethod.Get
                    };
                    HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                    string response = await httpResponseMessage.Content.ReadAsStringAsync();

                    var vehiclesResponse = JsonConvert.DeserializeObject<List<VehicleViewModel>>(response);
                    return vehiclesResponse!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting vehicles", ex);
            }
        }

        public async Task<string> AddVehicles(VehicleViewModel vehicleModel, string jwtToken)
        {
            try
            {
                var jsonStr = JsonConvert.SerializeObject(vehicleModel);
                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_configuration["ApiBaseAddress"] + "api/Vehicle/add", content);
                    string response = await httpResponseMessage.Content.ReadAsStringAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while adding vehicles to Database", ex);
            }
        }
      
        public async Task<string> UpdateVehicles(VehicleViewModel vehicleModel, string jwtToken)
        {
            try
            {
                var jsonStr = JsonConvert.SerializeObject(vehicleModel);
                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                    HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(_configuration["ApiBaseAddress"] + "api/Vehicle/edit", content);
                    string response = await httpResponseMessage.Content.ReadAsStringAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while editing vehicles to Database", ex);
            }
        }

        public async Task<string> DeleteVehicles(Guid vehicleId, string jwtToken)
        {
            try
            {
                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    var requestUri = $"{_configuration["ApiBaseAddress"]}api/Vehicle/delete/{vehicleId}";

                    HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(requestUri);
                    string response = await httpResponseMessage.Content.ReadAsStringAsync();
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while deleting vehicle", ex);
            }
        }

        public async Task<VehicleViewModel> GetVehicleByIdAsync(Guid vehicleId, string jwtToken)
        {
            try
            {
                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    var requestUri = new Uri($"{_configuration["ApiBaseAddress"]}api/Vehicle/get/{vehicleId}");
                    var response = await httpClient.GetAsync(requestUri);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<VehicleViewModel>(responseContent)!;
                    }
                    return null!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while fetching vehicle by Id");
            }
        }
    }
}
