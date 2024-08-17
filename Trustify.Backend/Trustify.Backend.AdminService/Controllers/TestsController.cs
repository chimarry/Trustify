using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Trustify.Backend.AdminService.Security;
using System.Net;
using Flurl.Http;
using Trustify.Backend.AdminService.Keycloak.DTO;

namespace Trustify.Backend.AdminService.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestsController : ControllerBase
    {

        private static HttpClient httpClient = new()
        {
        };

        [HttpGet("super-admin")]
        // [Authorize(Roles = "trustify.realm.super_administrator")]
        [Authorize(Policy = TrustifyPolicy.Authenticated)]
        public async Task<IActionResult> CheckSuperAdministrator()
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

            //HttpResponseMessage response;
            //string authorizeHeader = "Bearer " + accessToken;
            // /admin/realms/$REALM_NAME/clients?clientId=$CLIENT_ID
            //var response = new Flurl.Url("https://192.168.56.101:8443/admin/realms/trustify/clients?clientId=trustify_admin")
            //      .WithOAuthBearerToken(accessToken)
            //      .GetStringAsync();
            //return Ok(response);
            return Ok(authResult?.Principal);
            //string uri = "https://192.168.56.101:8443/admin/realms/trustify/clients?clientId=trustify_admin";
            //using (var request = new HttpRequestMessage())
            //{
            //    // Set the request method and URL
            //    request.Method = HttpMethod.Get;
            //    request.RequestUri = new Uri(uri);

            //    // Set individual request headers
            //    request.Headers.Add("Authorization", authorizeHeader);

            //    // Make the request
            //    response = await httpClient.SendAsync(request);
            //    if (response.StatusCode != HttpStatusCode.NoContent)
            //    {

            //        string content = await response.Content.ReadAsStringAsync();
            //        return Ok(content);
            //    }
            //}
            return BadRequest("failed");
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

            string uri = "https://192.168.56.101:8443/realms/trustify/protocol/openid-connect/userinfo";
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
        // [Authorize(Roles = "trustify.administrator")]
        [Authorize(Policy = "Restricted")]
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
