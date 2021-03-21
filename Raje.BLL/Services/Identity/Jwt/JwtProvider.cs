using Raje.DL.DB.Admin;
using Raje.DL.Services.BLL.Identity;
using Raje.Infra.Const;
using Raje.Infra.Enums;
using Raje.Infra.Util;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Raje.BLL.Services.Identity
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.Name),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(UserClaims.UserLastGuid,user.LastGuidAuthentication)
            };

            AddUserRoleClaims(claims, user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddMinutes(_jwtOptions.JwtExpireMinutes);

            var token = new JwtSecurityToken(
                _jwtOptions.JwtIssuer,
                _jwtOptions.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private void AddUserRoleClaims(List<Claim> claims, User user)
        {
            if (user.UserRole.Name == UserRoleTypes.AdminMaster.GetDescription())
                return;
        }

    }
}
