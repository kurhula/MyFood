using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseLayer.Enums.Auth;
using DataBaseLayer.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyFood_BE.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(AuthLevel.Admin))]
    public class UserController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager) => _userManager = userManager;


        /// <summary>
        /// Test user model
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            //var user = new AppUser
            //{
            //    FullName = "test",
            //    Email = "orbisalonzo@gmail.com",
            //    UserName = "orbisalonzo@gmail.com",
            //    UserType = DataBaseLayer.Enums.UserType.Client
            //};

            var result = await _userManager.FindByNameAsync("orbisalonzo@gmail.com");

            return Ok(result);
        }
    }
}