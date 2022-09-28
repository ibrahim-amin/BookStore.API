using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUP(SignUpModel signUpModel)
        {
            var result = accountRepository.SignupAsync(signUpModel);
            if (result.Result.Succeeded)
            {
                return Ok(result.Result.Succeeded);
            }
            return Unauthorized();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(SignInModel signInModel)
        {
            var result =  await accountRepository.LoginAsync(signInModel);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
