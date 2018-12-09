using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.AuthorizePolicy
{
    /// <summary>
    /// JwtToken生成
    /// </summary>
    public class JwtToken
    {
        public static dynamic BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(permissionRequirement.Expiration),
                signingCredentials: permissionRequirement.SigningCredentials
                );
            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new
            {
                status = true,
                access_token = encodeJwt,
                expires_in = permissionRequirement.Expiration.TotalMilliseconds,
                token_type = "Bearer"
            };
            return responseJson;
        }
    }
}
