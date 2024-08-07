using financias.src.interfaces;
using financiasapi.src.dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financias.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BankControllers : ControllerBase
    {
        private readonly IBankService _bankService;
        public BankControllers(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBank createBank)
        {
            var idUser = User.Claims.First(c => c.Type == "id").Value;
            createBank.UserId = Guid.Parse(idUser);

            await _bankService.Create(createBank);
            return Ok(new {});

        }

        [HttpDelete("id:guid")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bankService.Delete(id);
            return Ok("banck deleted success");
        }

        [HttpGet("id:guid")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var banckView = await _bankService.GetById(id);
            return Ok(banckView);
        }

        [HttpGet]
        public async Task<IActionResult> GetActive()
        {
            var idUser = User.Claims.First(c => c.Type == "id").Value;
            var bancksView = await _bankService.GetActive(Guid.Parse(idUser));
            return Ok(bancksView);
        }

        [HttpGet("userId:guid")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {

            var banckView = await _bankService.GetByUserId(userId);
            return Ok(banckView);
        }

        [HttpPut("id:guid")]
        public async Task<IActionResult> Update(Guid id, UpdateBank updateBank)
        {
            updateBank.Id = id;
            await _bankService.Update(updateBank);
            return NoContent();
        }

       
    }
}