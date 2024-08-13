using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using VehicleRegistrationWebApp.Models;

namespace VehicleRegistrationWebApp.Services
{
    public class VehicleService 
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VehicleService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<VehicleViewModel>> GetVehicles()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://localhost:7095/api/Vehicle/getAllVehicles"),
                    Method = HttpMethod.Get
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();

                var vehiclesResponse = JsonConvert.DeserializeObject<List<VehicleViewModel>>(response);
                return vehiclesResponse!;
            }
        }

        Task<IActionResult> AddVehicles()
        {
            throw new NotImplementedException();
        }

        Task<IActionResult> UpdateVehicles()
        {
            throw new NotImplementedException();
        }

        Task<IActionResult> DeleteVehicles()
        {
            throw new NotImplementedException();
        }

        Task<IActionResult> GetVehicleById()
        {
            throw new NotImplementedException();
        }
    }
}
