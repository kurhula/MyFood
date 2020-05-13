using BussinesLayer.UnitOfWork;
using DataBaseLayer.Models.Restaurants;
using DataBaseLayer.ViewModels.Rating;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MyFood_BE.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IUnitOfWork _services;
        public RestaurantController(IUnitOfWork services) => _services = services;

        [HttpGet]
        public async Task<IActionResult> GetAll(string name = null)
        {
            if (string.IsNullOrEmpty(name)) return Ok(await _services.RestaurantService.GetAllPaginated());
            return Ok(await _services.RestaurantService.GetAllPaginated(x => x.Title.Contains(name)));
        }


        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _services.RestaurantService.GetById(id);
            if (result == null) return NoContent();
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Restaurant model)
        {
            var result = await _services.RestaurantService.Update(model);
            if (!result) return BadRequest("Intente de nuevo mas tarde");
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _services.RestaurantService.SoftRemove(id);
            if (!result) return BadRequest("Intente de nuevo mas tarde");
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var result = await _services.RestaurantService.GetByUserId(userId);
            if (result == null) return BadRequest("Lo sentimos, este restaurante no existe");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(SetRating model)
        {
            var result = await _services.RestaurantService.SetRating(model.Id, model.Rating);
            if(!result) return BadRequest("Intente de nuevo mas tarde");
            return Ok(result);
        }
    }
}