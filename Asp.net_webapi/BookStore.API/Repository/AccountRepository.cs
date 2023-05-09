using BookStore.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.API.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser> signinManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signinManager=signinManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                Email = signUpModel.Email,
                Firstname=signUpModel.Firstname,
                Lastname=signUpModel.Lastname,
                UserName=signUpModel.Email

            };

            //password is creating using another method
            return await _userManager.CreateAsync(user,signUpModel.Password);
        }
        public async Task<string> LoginAsync(SignInModel signInModel)
        {
            var result = await _signinManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
