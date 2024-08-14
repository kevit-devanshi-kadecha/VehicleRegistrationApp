using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using VehicleRegistration.Infrastructure.DataBaseModels;
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
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri(_configuration["ApiBaseAddress"]+"api/Vehicle/getAllVehicles"),
                    Method = HttpMethod.Get
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();

                var vehiclesResponse = JsonConvert.DeserializeObject<List<VehicleViewModel>>(response);
                return vehiclesResponse!;
            }
        }

        public async Task<string> AddVehicles(VehicleViewModel vehicleModel)
        {
            var jsonStr = JsonConvert.SerializeObject(vehicleModel);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_configuration["ApiBaseAddress"] + "api/Vehicle/add", content);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                return response;
            }
        }
      
        public async Task<string> UpdateVehicles(VehicleViewModel vehicleModel)
        {
            var jsonStr = JsonConvert.SerializeObject(vehicleModel);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(_configuration["ApiBaseAddress"] + "api/Vehicle/edit", content);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                return response;
            }
        }

    }
}
