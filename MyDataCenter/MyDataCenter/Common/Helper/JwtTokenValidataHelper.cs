using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace MyDataCenter.Common.Helper
{
    public class JwtTokenValidataHelper : ISecurityTokenValidator
    {
        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }
        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            ClaimsPrincipal principal;
            try
            {
                validatedToken = null;
                //这里需要验证生成的Token
                /*
                    eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8y
                    MDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoid2FuZ3NoaWJhbmciLCJodHRwOi8vc2NoZW1hcy5ta
                    WNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiwgTWFuYWdlIi
                    wibmJmIjoxNTIyOTI0MDgxLCJleHAiOjE1MjI5MjU4ODEsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTA
                    wMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.fa0jDYt_MqHFcwQfsMS30eCsjEwQt_uiv96bGtMQJBE
                */
                var token = new JwtSecurityToken(securityToken);
                //获取到Token的一切信息
                var payload = token.Payload;
                var roles = (from t in payload where t.Key == ClaimTypes.Role select t.Value.ToString());
                var name = (from t in payload where t.Key == ClaimTypes.Name select t.Value).FirstOrDefault();
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, name.ToString()));
                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
                principal = new ClaimsPrincipal(identity);
            }
            catch
            {
                validatedToken = null;
                principal = null;
            }
            return principal;
        }
    }
}
