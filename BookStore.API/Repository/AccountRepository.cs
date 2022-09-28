using BookStore.API.Models;
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

namespace BookStore.API.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;


        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IConfiguration configuration)
        {
            this._userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public async Task<IdentityResult> SignupAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                LastName = signUpModel.LastName,
                FirstName = signUpModel.FirstName,
                Email = signUpModel.Email,
                UserName=signUpModel.Email,
            };
            var result =await _userManager.CreateAsync(user,signUpModel.Password);
            if (result.Succeeded)
            {
                return result;
            }
            else
            {
                var error = result.Errors;
            }
            
           
            return result;
        }


        public async Task<string> LoginAsync(SignInModel signInModel)
        {
            var result = await signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;

            }
            //var claims = new Claim[]
            var AuthClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
             issuer: configuration["JWT:ValidIssuer"],
             audience: configuration["JWT:ValidAudience"],
             expires: DateTime.Now.AddDays(1),
             claims: AuthClaims,
             signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
             );

          return  new JwtSecurityTokenHandler().WriteToken(token);


        }

    }
}
