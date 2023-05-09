using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AccountController : ControllerBase
    {       private IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepositorycs)
        {
            _accountRepository = accountRepositorycs;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel signUpModel)
        {
            var result =await _accountRepository.SignUpAsync(signUpModel);

            if(result.Succeeded)
            {
                return Ok();
            }
            return Unauthorized();

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel)
        {
            var result = await _accountRepository.LoginAsync(signInModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
