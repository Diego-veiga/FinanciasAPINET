using System.Text.Json;
using financias.src.commands.BankAccount;
using financias.src.query.BankAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace financias.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Create(CreateBankAccountCommand createBanckAcconutCommand)
        {  

            _logger.LogInformation($"Start endepoint Create with object {JsonSerializer.Serialize(createBanckAcconutCommand)}");

            createBanckAcconutCommand.UserId = Guid.Parse(User.Claims.First(c => c.Type == "id").Value);

            await _mediator.Send(createBanckAcconutCommand);
            return Created();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(UpdateBankAccountCommand updateBanckAcconutCommand, Guid id)
        {
            _logger.LogInformation($"Start endepoint Update with object {JsonSerializer.Serialize(updateBanckAcconutCommand)} and params {id}");
            updateBanckAcconutCommand.Id = id;

            await _mediator.Send(updateBanckAcconutCommand);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"Start endepoint Delete with params {id}");
            var deleteBanckAcconutCommand = new DeleteBankAccountCommand() { Id = id };

            await _mediator.Send(deleteBanckAcconutCommand);
            return NoContent();
        }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation($"Start endepoint GetById with params {id}");

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

            _logger.LogInformation($"Start endepoint GetBankAccountAllByUserIdHandler return object {JsonSerializer.Serialize(results)}");

            return results is null ? NotFound("BankAccount Not found") : Ok(results);
        }

    }
}