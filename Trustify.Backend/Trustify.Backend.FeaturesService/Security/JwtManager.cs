using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductManager.Core.ErrorHandling;
using ProductManager.WebAPI.Models.Options;
using ProductManager.WebAPI.Models.User;
using ProductManager.WebAPI.Util;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProductManager.WebAPI.Security
{
    /// <summary>
    /// Util methods for working with JWT token. 
    /// </summary>
    public class JwtManager : IJwtManager
    {
        private readonly AuthenticationOptions options;

        public JwtManager(IOptions<AuthenticationOptions> options)
        {
            this.options = options.Value;
        }

        /// <summary>
        /// Based on provided user's information, creates ClaimsIdentity with claims: 
        /// username, full name and user id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ClaimsIdentity CreateSubject(UserWrapper user)
            => new(new Claim[]
                   {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.GivenName, user.FullName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                   });

        /// <summary>
        /// Creates JWT token that contains claims identity information. Expiration date for JWT token is
        /// read from provided options.
        /// </summary>
        /// <param name="subject">Identity for which JWT token is generated</param>
        /// <returns></returns>
        public string GenerateJWTToken(ClaimsIdentity? subject)
        {
            if (subject == null)
                throw new UnauthorizedException();

            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(options.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddMinutes(options.AccessTokenLifetimeMin),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Creates refresh token with certain expiration date.
        /// </summary>
        /// <param name="ipAddress">IP address that refreshes token</param>
        /// <returns></returns>
        public RefreshTokenWrapper GenerateRefreshToken(string ipAddress)
        {
            byte[] randomBytes = new byte[64];

            RandomNumberGenerator.Create().GetBytes(randomBytes);
            return new RefreshTokenWrapper
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiresOn = DateTime.UtcNow.AddDays(options.RefreshTokenLifetimeDay),
                CreatedOn = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        public ClaimsPrincipal GetPrincipalFromToken(string encodedAccessToken)
        {
            if (ParseToken(encodedAccessToken, false) is not JwtSecurityToken parsedToken)
                throw new UnauthorizedException();

            Claim[] claims = new[]
            {
                 new Claim(ClaimTypes.Name, parsedToken.Claims.First(x => x.Type==HttpConstants.ClaimName).Value),
                 new Claim(ClaimTypes.GivenName, parsedToken.Claims.First(x=>x.Type == HttpConstants.ClaimGivenName).Value),
                 new Claim(ClaimTypes.NameIdentifier, parsedToken.Claims.First(x=>x.Type==HttpConstants.ClaimNameIdentifier).Value)
            };

            var claimsIdentity = new ClaimsIdentity(claims, Schemes.JwtScheme);
            return new ClaimsPrincipal(claimsIdentity);
        }

        /// <summary>
        /// Parses JWT token from provided base64 encoded authorization "Bearer" string. 
        /// Unauthorized exception will be thrown if provided string is not bearer authentication and 
        /// will be thrown if token is not JWT token.
        /// </summary>
        /// <param name="bearerToken"></param>
        /// <param name="validateExpire">Should expiration date be validated? (For example, when refreshing a token, it should not)</param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedException"></exception>
        public SecurityToken ParseToken(string bearerToken, bool validateExpire = true)
        {
            try
            {
                if (!bearerToken.StartsWith(HttpConstants.Bearer))
                    throw new UnauthorizedException();
                string token = bearerToken.Remove(0, HttpConstants.Bearer.Length);
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = validateExpire
                }, out SecurityToken validatedToken);

                return validatedToken;
            }
            catch
            {
                throw new UnauthorizedException();
            }
        }
    }
}
