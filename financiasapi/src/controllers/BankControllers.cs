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
    public class BankController : ControllerBase
    {
        public IMediator _mediator { get; set; }
        public ILogger<BankController> _logger { get; set; }
        private readonly IBankService _bankService;
        public BankController(IMediator mediator,IBankService bankService, ILogger<BankController> logger )
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
            
           return new CreatedAtRouteResult("GetBankAccount", new { id = createBankCommand.UserId }, createBankCommand);

        }

        [HttpDelete("id:guid")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteBankCommand = new DeleteBankCommand(){ Id = id};
  
            _logger.LogInformation($"Start endpoint Create with object {JsonSerializer.Serialize(deleteBankCommand)}");
            await _mediator.Send(deleteBankCommand);
            return Ok("Bank deleted success");
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
        public async Task<IActionResult> Update(Guid id, UpdateBankCommand updateBankCommand)
        {
            _logger.LogInformation($"Start endpoint Update with object {JsonSerializer.Serialize(updateBankCommand)}");
            _logger.LogInformation($"Start id authenticated {JsonSerializer.Serialize(id)}");
            updateBankCommand.Id = id;
            await _mediator.Send(updateBankCommand);
            
             return Ok();
        }

       
    }
}