using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using VehicleRegistration.Infrastructure.DataBaseModels;
using VehicleRegistrationWebApp.Models;

namespace VehicleRegistrationWebApp.Services
{
    public class AccountService 
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> SignUpAsync(SignUpViewModel model)
        {
            var jsonStr = JsonConvert.SerializeObject(model);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("https://localhost:7095/signup", content);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                return response;
            }
        }
        public async Task<LoginViewModel> LoginAsync(LoginViewModel model)
        {
            var jsonStr = JsonConvert.SerializeObject(model);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("https://localhost:7095/login", content);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();

                LoginViewModel loginResponse = JsonConvert.DeserializeObject<LoginViewModel>(response);
                return loginResponse;
            }
        }
    }
}
