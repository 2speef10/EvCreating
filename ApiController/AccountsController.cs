using EvCreating.ApiModels;
using EvCreating.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupSpace23.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        SignInManager<EvCreatingUser> _signInManager;

        public AccountsController(SignInManager<EvCreatingUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        [HttpGet("{name}/{password}")]
        // Mag vanzelfsprekend niet gebruikt worden!!!
        public async Task<ActionResult<Boolean>> LoginGet(string name, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(name, password, false, lockoutOnFailure: false);

            return result.Succeeded;
        }


        [HttpPost]
        [Route("Login")]
        [Route("/api/Login")]
        public async Task<ActionResult<Boolean>> PutAccount([FromBody] LoginModel @login)
        {
            var result = await _signInManager.PasswordSignInAsync(@login.Name, @login.Password, false, lockoutOnFailure: false);

            return result.Succeeded;
        }

    }
}