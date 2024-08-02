using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;

namespace Trustify.Backend.AdminService.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static HttpClient httpClient = new()
        {
        };

        public UsersController()
        {
        }

        [HttpGet("super-admin")]
        [Authorize(Roles = "trustify.superAdministrator")]
        public async Task<IActionResult> CheckSuperAdministrator()
        {
            return Ok();
        }

        [HttpGet("user-info")]
        [Authorize(Roles = "trustify.administrator, trustify.superAdministrator")]
        public async Task<IActionResult> GetUserInfo()
        {
            var authResult = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
            if (authResult?.Succeeded != true)
            {
                // Handle failed authentication
                return BadRequest("Failed auth");
            }

            // Get the access token and refresh token
            string? accessToken = authResult?.Properties?.GetTokenValue("access_token");
            if (accessToken == null)
                return BadRequest();

            string uri = "http://192.168.56.101:8080/realms/trustify/protocol/openid-connect/userinfo";
            string authorizeHeader = "Bearer " + accessToken;
            using (var request = new HttpRequestMessage())
            {
                // Set the request method and URL
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(uri);

                // Set individual request headers
                request.Headers.Add("Authorization", authorizeHeader);

                // Make the request
                HttpResponseMessage response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task Login(string returnUrl)
        {
            try
            {
                // in any case we want user to be redirected to refreshCsrfToken endpoint after the successful login
                // so that a correct CSRF token is generated
                // it can't be done in the scope of this request as context.User.Identity is still not properly set!
                // https://github.com/aspnet/AspNetCore/issues/2783
                var request = this.HttpContext.Request;
                var postLoginBaseUri = request.Scheme + "://" + request.Host + request.PathBase + "/users/login-callback";
                if (string.IsNullOrEmpty(returnUrl) || returnUrl.Contains("refreshCsrfToken", StringComparison.InvariantCultureIgnoreCase))
                {
                    // if returnUrl is not set or already includes refreshCsrfToken endpoint (which is very unlikely)
                    // then set to base app path
                    returnUrl = request.Scheme + "://" + request.Host + request.PathBase;
                }
                else
                {
                    returnUrl = QueryHelpers.AddQueryString(postLoginBaseUri, "returnUrl", returnUrl);
                }

                await this.HttpContext.ChallengeAsync("Cookies", new AuthenticationProperties() { RedirectUri = returnUrl });
            }
            catch (TaskCanceledException)
            {
                // This happens regularly because of the multiple async requests from ui
                // this is not a problem so we can ignore it
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("IDX20803", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw;
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "trustify.administrator")]
        public IActionResult AdminPanel()
        {

            return Ok();
        }

        [HttpGet("login-callback")]
        public async Task<IActionResult> LoginCallback()
        {
            var authResult = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
            if (authResult?.Succeeded != true)
            {
                // Handle failed authentication
                return BadRequest("Failed auth");
            }

            // Get the access token and refresh token
            var accessToken = authResult.Properties.GetTokenValue("access_token");
            var refreshToken = authResult.Properties.GetTokenValue("refresh_token");

            // Save the tokens to the user's session or database

            // Redirect the user to the desired page
            return Ok();
        }

        [HttpGet("access-denied")]
        public async Task<IActionResult> AccessDenied()
        {


            var authResult = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
            if (authResult?.Succeeded != true)
            {
                // Handle failed authentication
                return BadRequest("Failed auth");
            }

            // Get the access token and refresh token
            var accessToken = authResult.Properties.GetTokenValue("access_token");
            var refreshToken = authResult.Properties.GetTokenValue("refresh_token");

            // Save the tokens to the user's session or database
            //  HttpContext.Session.SetString("access_token", accessToken);
            // HttpContext.Session.SetString("refresh_token", refreshToken);
            return BadRequest("Access denied");
        }
    }
}
