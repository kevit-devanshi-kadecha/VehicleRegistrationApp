using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using VehicleRegistrationWebApp.Models;

namespace VehicleRegistrationWebApp.Services
{
    public class AccountService 
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AccountService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> SignUpAsync(SignUpViewModel model)
        {
            _logger.LogInformation("SignUp method for sending api request executed");
            var jsonStr = JsonConvert.SerializeObject(model);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_configuration["ApiBaseAddress"] + "signup", content);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                _logger.LogInformation($"{response}");
                return response;
            }
        }
        public async Task<TokenResponse> LoginAsync(LoginViewModel model, HttpContext httpContext)
        {
            _logger.LogDebug($"Username: {model.UserName}");
            _logger.LogInformation("Login method for sending api request executed");

            var jsonStr = JsonConvert.SerializeObject(model);
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(_configuration["ApiBaseAddress"] + "login", content);
                
                _logger.LogInformation($"Response: {httpResponseMessage.StatusCode.ToString()}");

                if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    return new TokenResponse
                    {
                        Message = responseContent
                    };
                }

                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                TokenResponse loginResponse = JsonConvert.DeserializeObject<TokenResponse>(response)!;
                httpContext.Session.SetString("Token", loginResponse.JwtToken);
                return loginResponse;
            }
        }

        public async Task<(bool success, string FilePath)> UploadFileAsync(IFormFile file, string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                throw new ArgumentNullException(nameof(jwtToken), "JWT token cannot be null or empty.");
            }
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                try
                {
                    var multipartContent = new MultipartFormDataContent();

                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                    multipartContent.Add(fileContent, "file", file.FileName);

                    HttpResponseMessage response = await httpClient.PostAsync(_configuration["ApiBaseAddress"] + "uploadImage", multipartContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var responsePath = await response.Content.ReadAsStringAsync();
                        return (true, responsePath);
                    }
                    else
                    {
                        return (false, null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occured {ex}");
                    return (false, null);
                }
            }
        }
    }
}




