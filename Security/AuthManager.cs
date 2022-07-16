using HotelListing.Data;
using HotelListing.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Security
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly IConfiguration configuration;
        private ApiUser apiUser;
        public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            
        }
       
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSetting = configuration.GetSection("JWT");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSetting.GetSection("lifetime").Value));
            var token = new JwtSecurityToken(
                issuer: jwtSetting.GetSection("Issuer").Value,
                claims:claims,
                expires:expiration,
                signingCredentials:signingCredentials
                
                );
            return token;

        }

        public async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.Name,apiUser.UserName)                
           };
            var roles = await userManager.GetRolesAsync(apiUser);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));               
            }
            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("key");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<bool> ValidateUser(LoginDTO userDTO)
        {
            apiUser = await userManager.FindByNameAsync(userDTO.Email);
            return (apiUser != null && await userManager.CheckPasswordAsync(apiUser, userDTO.Password));
        }


        //public class MyClaimsTransformation : IClaimsTransformation
        //    {
        //        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        //        {
        //            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
        //            var claimType = "myNewClaim";
        //            if (!principal.HasClaim(claim => claim.Type == claimType))
        //            {
        //                claimsIdentity.AddClaim(new Claim(claimType, "myClaimValue"));
        //            }

        //            principal.AddIdentity(claimsIdentity);
        //            return Task.FromResult(principal);
        //        }
        //    }

    }

}
