using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Itechart.Common;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Itechart.Survey.WebApi.Models
{
    [UsedImplicitly]
    public class SurveyJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer;


        public SurveyJwtFormat(string issuer)
        {
            _issuer = issuer;
        }


        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var audienceId = ConfigurationManager.AppSettings["AudienceId"];
            var symmetricKeyAsBase64 = ConfigurationManager.AppSettings["AudienceSecret"];
            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
            var signingKey = new SigningCredentials(new SymmetricSecurityKey(keyByteArray), SecurityAlgorithms.HmacSha256);
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
