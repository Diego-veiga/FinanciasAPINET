using System.Text.Json;
using financias.src.commands.BankAccount;
using financias.src.query.BankAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financias.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
     [Authorize]
    public class BankAccountController : ControllerBase
    {
        public IMediator _mediator { get; set; }
        public ILogger<BankAccountController> _logger { get; set; }
    
        public BankAccountController(IMediator mediator,ILogger<BankAccountController> logger)
        {
            _mediator = mediator;
            _logger =logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBankAccountCommand createBankAccountCommand)
        {  

            _logger.LogInformation($"Start endpoint Create with object {JsonSerializer.Serialize(createBankAccountCommand)}");

            createBankAccountCommand.UserId = Guid.Parse(User.Claims.First(c => c.Type == "id").Value);

            await _mediator.Send(createBankAccountCommand);
            return Created();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(UpdateBankAccountCommand updateBankAccountCommand, Guid id)
        {
            _logger.LogInformation($"Start endpoint Update with object {JsonSerializer.Serialize(updateBankAccountCommand)} and params {id}");
            updateBankAccountCommand.Id = id;

            await _mediator.Send(updateBankAccountCommand);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"Start endpoint Delete with params {id}");
            var deleteBankAccountCommand = new DeleteBankAccountCommand() { Id = id };

            await _mediator.Send(deleteBankAccountCommand);
            return NoContent();
        }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation($"Start endpoint GetById with params {id}");

            var getBankAccountById = new GetBankAccountById() { Id = id };

            _logger.LogInformation($"object created getBankAccountAllByUserId for the mediator to send {JsonSerializer.Serialize(getBankAccountById)}");

            var result = await _mediator.Send(getBankAccountById);

            _logger.LogInformation($"Start endpoint GetBankAccountByIdHandler return object {JsonSerializer.Serialize(result)}");

            return result is null ? NotFound("BankAccount Not found") : Ok(result);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Start endpoint GetAll");

            var getBankAccountAllByUserId = new GetBankAccountAllByUserId()
            {
                    UserId = Guid.Parse(User.Claims.First(c => c.Type == "id").Value)
            };
            
            _logger.LogInformation($"object created getBankAccountAllByUserId for the mediator to send {JsonSerializer.Serialize(getBankAccountAllByUserId)}");

            var results = await _mediator.Send(getBankAccountAllByUserId);

            _logger.LogInformation($"Start endpoint GetBankAccountAllByUserIdHandler return object {JsonSerializer.Serialize(results)}");

            return results is null ? NotFound("BankAccount Not found") : Ok(results);
        }

    }
}