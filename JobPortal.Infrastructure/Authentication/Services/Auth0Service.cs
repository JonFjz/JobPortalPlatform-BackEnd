using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Application.Features.JobSeeker.Dtos;
using JobPortal.Application.Helpers.Models.Auth0;
using JobPortal.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace JobPortal.Infrastructure.Authentication.Services
{
    public class Auth0Service : IAuth0Service
    {
        private readonly HttpClient _httpClient;
        private readonly Auth0Settings _auth0Settings;
        private readonly IUserRegistrationService _userRegistrationService;

        public Auth0Service(HttpClient httpClient, IOptions<Auth0Settings> auth0Settings, IUserRegistrationService userRegistrationService)
        {
            _httpClient = httpClient;
            _auth0Settings = auth0Settings.Value;
            _userRegistrationService = userRegistrationService;
        }


        #region Register
        private async Task<string> SignupAsync(string email, string password, string userType)
        {
            var signupRequest = new
            {
                client_id = _auth0Settings.ClientId,
                email,
                password,
                connection = _auth0Settings.Connection,
                user_metadata = new { type = userType }
            };

            var response = await _httpClient.PostAsJsonAsync(_auth0Settings.SignupEndpoint, signupRequest);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var userInfo = JsonDocument.Parse(jsonResponse);

            string userId = userInfo.RootElement.GetProperty("_id").GetString();
            return $"auth0|{userId}";
        }

        public async Task<string> SignupJobSeekerAsync(JobSeekerRegistrationDto request, string userType)
        {
            var auth0Id = await SignupAsync(request.Email, request.Password, userType);
            await _userRegistrationService.SaveJobSeekerAsync(request, auth0Id);
            return "Job seeker registration successful.";
        }

        public async Task<string> SignupEmployerAsync(EmployerRegistrationDto request, string userType)
        {
            var auth0Id = await SignupAsync(request.Email, request.Password, userType);
            await _userRegistrationService.SaveEmployerAsync(request, auth0Id);
            return "Employer registration successful.";
        }

        #endregion


        #region Get Token
        public async Task<string> GetTokenAsync(Auth0TokenRequest request)
        {
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, $"{_auth0Settings.Domain}/oauth/token")
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", request.Email),
                    new KeyValuePair<string, string>("password", request.Password),
                    new KeyValuePair<string, string>("audience", _auth0Settings.Audience),
                    new KeyValuePair<string, string>("client_id", _auth0Settings.ClientId),
                    new KeyValuePair<string, string>("client_secret", _auth0Settings.ClientSecret),
                })
            };

            tokenRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(tokenRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        #endregion
    }
}