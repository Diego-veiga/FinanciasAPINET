
using financias.src.DTOs;
using financias.src.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financias.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BanckControllers : ControllerBase
    {
        private readonly IBanckService _banckService;
        public BanckControllers(IBanckService banckService)
        {
            _banckService = banckService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBanck createBanck)
        {
            var idUser = User.Claims.First(c => c.Type == "id").Value;
            createBanck.UserId = Guid.Parse(idUser);

            await _banckService.Create(createBanck);
            return Ok();

        }

        [HttpDelete("id:guid")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _banckService.Delete(id);
            return Ok("banck deleted success");
        }

        [HttpGet("id:guid")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var banckView = await _banckService.GetById(id);
            return Ok(banckView);
        }

        [HttpGet]
        public async Task<IActionResult> GetActive()
        {
            var idUser = User.Claims.First(c => c.Type == "id").Value;
            var bancksView = await _banckService.GetActive(Guid.Parse(idUser));
            return Ok(bancksView);
        }

        [HttpGet("userId:guid")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {

            var banckView = await _banckService.GetByUserId(userId);
            return Ok(banckView);
        }

        [HttpPut("id:guid")]
        public async Task<IActionResult> Update(Guid id, UpdateBanck updateBanck)
        {
            updateBanck.Id = id;
            await _banckService.Update(updateBanck);
            return NoContent();
        }

       
    }
}