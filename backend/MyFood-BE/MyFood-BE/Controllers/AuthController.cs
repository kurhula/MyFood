using BussinesLayer.UnitOfWork;
using DataBaseLayer.Models.Users;
using DataBaseLayer.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyFood_BE.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _services;
        private readonly UserManager<AppUser> _user;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthController(IUnitOfWork services, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _services = services;
            _user = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> BuildToken(LoginUserVm user)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            if (!loginResult.Succeeded) return BadRequest($"Credenciales incorrectas intente de nuevo");
            var token = await _services.AuthService.BuildToken(user);
            return Ok(new { Token = token, user.UserName });
        }
    }
}