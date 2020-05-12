using AutoMapper;
using BussinesLayer.UnitOfWork;
using DataBaseLayer.Enums.Auth;
using DataBaseLayer.Models.Users;
using DataBaseLayer.Options;
using DataBaseLayer.ViewModels.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MyFood_BE.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(AuthLevel.Admin))]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _services;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(AuthLevel.Admin) + "," + nameof(AuthLevel.Restaurant))]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]FilterUserVM model = null)
            => Ok(await _services.UserService.GetAllPaginated(model));

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVM model)
        {
            var result = await _services.UserService.CreateUser(model);
            if (!result) return BadRequest("Error, Intente de nuevo mas tarde");
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("id invalido");
            var result = await _services.UserService.GetUser(id);
            if (result == null) return NoContent();
            return Ok(_mapper.Map<UserVM>(result));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("Email invalido");
            var result = await _services.UserService.GetByEmail(email);
            if (result == null) return NoContent();
            return Ok(_mapper.Map<UserVM>(result));
        }

        [HttpPut]
        public async Task<IActionResult> Update(AppUser user)
        {
            var result = await _services.UserService.Update(user);
            if (!result) return BadRequest("Lo sentimos, intente de nuevo mas tarde");
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Operación invalida intente de nuevo");
            var result = await _services.UserService.SoftDelete(id);
            if (!result) return BadRequest("Lo sentimos, intente de nuevo mas tarde");
            return Ok(result);
        }

    }
}