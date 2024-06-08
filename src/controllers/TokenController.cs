using financias.src.DTOs;
using financias.src.interfaces;
using financias.src.models;
using Microsoft.AspNetCore.Mvc;

namespace financias.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICachingService _cachingService;

        public TokenController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Token(Login login)
        {
            var token = await _userService.Login(login);
            return Ok(new ResponseHandlerSuccess() { Data = token });

        }
    }
}