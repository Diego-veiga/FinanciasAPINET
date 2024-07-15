using System.Text.Json;
using System.Text.Json.Serialization;
using financias.src.commands.BanckAccount;
using financias.src.query.BanckAccount;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace financias.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BanckAccountController : ControllerBase
    {
        public IMediator _mediator { get; set; }
        public ILogger<BanckAccountController> _logger { get; set; }
    
        public BanckAccountController(IMediator mediator,ILogger<BanckAccountController> logger)
        {
            _mediator = mediator;
            _logger =logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBanckAcconutCommand createBanckAcconutCommand)
        {  

            _logger.LogInformation($"Start endepoint Create with object {JsonSerializer.Serialize(createBanckAcconutCommand)}");

            createBanckAcconutCommand.UserId = Guid.Parse(User.Claims.First(c => c.Type == "id").Value);

            await _mediator.Send(createBanckAcconutCommand);
            return Created();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(UpdateBanckAcconutCommand updateBanckAcconutCommand, Guid id)
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
            var deleteBanckAcconutCommand = new DeleteBanckAcconutCommand() { Id = id };

            await _mediator.Send(deleteBanckAcconutCommand);
            return NoContent();
        }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation($"Start endepoint GetById with params {id}");

            var getBanckAccountById = new GetBanckAccountById() { Id = id };

            _logger.LogInformation($"object created getBanckAccountAllByUserId for the mediator to send {JsonSerializer.Serialize(getBanckAccountById)}");

            var result = await _mediator.Send(getBanckAccountById);

            _logger.LogInformation($"Start endepoint GetBanckAccountByIdHandler return object {JsonSerializer.Serialize(result)}");

            return result is null ? NotFound("BanckAccount Not found") : Ok(result);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Start endepoint GetAll");

            var getBanckAccountAllByUserId = new GetBanckAccountAllByUserId()
            {
                    UserId = Guid.Parse(User.Claims.First(c => c.Type == "id").Value)
            };
            
            _logger.LogInformation($"object created getBanckAccountAllByUserId for the mediator to send {JsonSerializer.Serialize(getBanckAccountAllByUserId)}");

            var results = await _mediator.Send(getBanckAccountAllByUserId);

            _logger.LogInformation($"Start endepoint GetBanckAccountAllByUserIdHandler return object {JsonSerializer.Serialize(results)}");

            return results is null ? NotFound("BanckAccount Not found") : Ok(results);
        }

    }
}