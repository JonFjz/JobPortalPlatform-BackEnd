using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Helpers.Models.Auth0;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace JobPortal.Infrastructure.Authentication.Services
{
    public class Auth0Service : IAuth0Service
    {
        private readonly HttpClient _httpClient;
        private readonly Auth0Settings _auth0Settings;
        private readonly IUnitOfWork _unitOfWork;

        public Auth0Service(HttpClient httpClient,
                            IOptions<Auth0Settings> auth0Settings,
                            IUnitOfWork unitOfWork)
        {
            _httpClient = httpClient;
            _auth0Settings = auth0Settings.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> SignupUserAsync(Auth0SignupRequest request, string userType)
        {
            var signupRequest = new
            {
                client_id = _auth0Settings.ClientId,
                username = request.Username,
                email = request.Email,
                password = request.Password,
                connection = _auth0Settings.Connection,
                user_metadata = new { type = userType }
            };

            var response = await _httpClient.PostAsJsonAsync( _auth0Settings.SignupEndpoint, signupRequest);
            return await HandleResponseAsync(response, "Failed to signup user");
        }



        public async Task<string> GetTokenAsync(Auth0TokenRequest request)
        {
            var tokenRequest = new HttpRequestMessage( HttpMethod.Post, $"{_auth0Settings.Domain}/oauth/token")
            {
                Content = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username",request.Email),  // email
                    new KeyValuePair<string, string>("password", request.Password),
                    new KeyValuePair<string, string>("audience",_auth0Settings.Audience),
                    new KeyValuePair<string, string>("client_id", _auth0Settings.ClientId),
                    new KeyValuePair<string, string>("client_secret",_auth0Settings.ClientSecret),
                })
            };
            tokenRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(tokenRequest);
            var tokenResponse =await HandleResponseAsync(response, "Failed to get token");

            return tokenResponse;
        }


        private static async Task<string> HandleResponseAsync(HttpResponseMessage response, string errorMessage)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(
                    $"{errorMessage}. Status code: {response.StatusCode}, Error: {errorContent}");
            }
        }
    }
}
