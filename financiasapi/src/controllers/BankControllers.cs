using financias.src.interfaces;
using financiasapi.src.commands.Bank;
using financiasapi.src.dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace financias.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BankControllers : ControllerBase
    {
        public IMediator _mediator { get; set; }
        public ILogger<BankControllers> _logger { get; set; }
        private readonly IBankService _bankService;
        public BankControllers(IMediator mediator,IBankService bankService, ILogger<BankControllers> logger )
        {
            _mediator = mediator;
            _bankService = bankService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBankCommand createBankCommand)
        {
            
            _logger.LogInformation($"Start endpoint Create with object {JsonSerializer.Serialize(createBankCommand)}");
            var idUser = User.Claims.First(c => c.Type == "id").Value;
            _logger.LogInformation($"Start UserId authenticated {JsonSerializer.Serialize(idUser)}");
            createBankCommand.UserId = Guid.Parse(idUser);
            await _mediator.Send(createBankCommand);

            //await _bankService.Create(createBank);
           return new CreatedAtRouteResult("GetBankAccount", new { id = createBankCommand.UserId }, createBankCommand);

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