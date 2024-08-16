using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using VehicleRegistrationWebApp.Models;

namespace VehicleRegistrationWebApp.Services
{
    public class AccountService 
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AccountService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> SignUpAsync(SignUpViewModel model)
        {
            var jsonStr = JsonConvert.SerializeObject(model);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_configuration["ApiBaseAddress"] + "signup", content);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                return response;
            }
        }
        public async Task<TokenResponse> LoginAsync(LoginViewModel model, HttpContext httpContext)
        {
            var jsonStr = JsonConvert.SerializeObject(model);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_configuration["ApiBaseAddress"] + "login", content);

                if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    return new TokenResponse
                    {
                        Message = responseContent
                    };
                }

                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                TokenResponse loginResponse = JsonConvert.DeserializeObject<TokenResponse>(response);
                httpContext.Session.SetString("Token", loginResponse.JwtToken);
                return loginResponse;
            }
        }
    }
}
