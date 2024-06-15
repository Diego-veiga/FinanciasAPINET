using financias.src.commands.BanckAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace financias.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BanckAccountController : ControllerBase
    {
        public IMediator _mediator { get; set; }
        public BanckAccountController(IMediator mediator)
        {
            _mediator=mediator; 
        }

       [HttpPost]
        public async Task<IActionResult> Create(CreateBanckAcconutCommand createBanckAcconutCommand){

            createBanckAcconutCommand.UserId = Guid.Parse(User.Claims.First(c => c.Type == "id").Value); 

            await _mediator.Send(createBanckAcconutCommand);
            return Created();
        }
    }
}