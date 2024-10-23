using financias.src.interfaces;
using financiasapi.src.commands.user;
using financiasapi.src.dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace financias.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMediator _mediator;
    public UserController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserCommand createUserCommand)
        {
            await  _mediator.Send(createUserCommand);
            return Ok(new { message = "User created successfully" });
        }

        [HttpPut("id:guid")]
        public async Task<ActionResult> Update(Guid id, UpdateUser updateUser)
        {

            updateUser.Id = id;
            await _userService.Update(updateUser);
            return Ok(new { message = "User updated successfully" });

        }

        [HttpDelete("id:guid")]
        public async Task<ActionResult> Delete(Guid id)
        {

            var deleteUser = new DeleteUser() { Id = id };
            await _userService.Delete(deleteUser);
            return Ok(new { message = "User removed" });

        }

        [HttpGet("id:guid")]
        public async Task<ActionResult> GetById(Guid id)
        {

            var user = await _userService.GetById(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll()
        {

            var user = await _userService.GetActive();
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);

        }

    }

}
