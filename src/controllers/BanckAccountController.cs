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

   
            _logger.LogInformation($"Start endepoint create with object {JsonSerializer.Serialize(createBanckAcconutCommand)}");

            createBanckAcconutCommand.UserId = Guid.Parse(User.Claims.First(c => c.Type == "id").Value);
          

            await _mediator.Send(createBanckAcconutCommand);
            return Created();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(UpdateBanckAcconutCommand updateBanckAcconutCommand, Guid id)
        {
            updateBanckAcconutCommand.Id = id;

            await _mediator.Send(updateBanckAcconutCommand);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var deleteBanckAcconutCommand = new DeleteBanckAcconutCommand() { Id = id };

            await _mediator.Send(deleteBanckAcconutCommand);
            return NoContent();
        }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var getBanckAccountById = new GetBanckAccountById() { Id = id };

            var result = await _mediator.Send(getBanckAccountById);
            return result is null ? NotFound("BanckAccount Not found") : Ok(result);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var getBanckAccountAllByUserId = new GetBanckAccountAllByUserId()
            {
                    UserId = Guid.Parse(User.Claims.First(c => c.Type == "id").Value)
            };

            var result = await _mediator.Send(getBanckAccountAllByUserId);
            return result is null ? NotFound("BanckAccount Not found") : Ok(result);
        }

    }
}