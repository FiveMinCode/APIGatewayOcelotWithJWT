﻿using Auth.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.API
{
    public class JWTTokenService
    {
        private List<User> _users = new List<User>()
        {
            new User() {  Username= "admin", Password= "admin", Role="Administrator", Scopes = new[] { "product.read" } },
            new User() {  Username= "user1", Password= "user1", Role="User", Scopes = new[] { "product.read" } }
        };

        public Authentication GetAuthToken(Login loginModel) 
        { 
            var user = _users.FirstOrDefault(t => t.Username == loginModel.UserName && t.Password == loginModel.Password);

            if (user == null)
            {
                return null;
            }

            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a1b2c3d4e5f67890abcdef1234567890a1b2c3d4e5f67890abcdef1234567890"));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var expirationTimestamp = DateTime.Now.AddMinutes(1);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim("role", user.Role),
                new Claim("scope", string.Join("", user.Scopes))
            };


            var tokenOption = new JwtSecurityToken(
                issuer: "https://localhost:7106",
                audience: "https://localhost:7106",
                claims: claims,
                expires: expirationTimestamp,
                signingCredentials: signinCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);
            return new Authentication
            {
                Token = tokenString,
                ExpiresIn =
                (int)expirationTimestamp.Subtract(DateTime.Now).TotalSeconds,
                RefreshToken = GenerateRefreshToken()
            };
        }

        public string AuthToken(IEnumerable<Claim> claims)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a1b2c3d4e5f67890abcdef1234567890a1b2c3d4e5f67890abcdef1234567890"));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var tokenOption = new JwtSecurityToken(
                issuer: "https://localhost:7106",
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: signinCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a1b2c3d4e5f67890abcdef1234567890a1b2c3d4e5f67890abcdef1234567890")),
                ValidateLifetime = false
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenhandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            return principal;
        }
    }
}
