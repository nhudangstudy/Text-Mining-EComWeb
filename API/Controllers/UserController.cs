﻿using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpPost("{email}")]
        [ProducesResponseType(typeof(GetByIdPublicAccountModel), 200)]
        public async Task<IActionResult> CreateUserAync(string email, CreateUpdateUserRequestModel createUserRequest)
        {
            await _userService.CreateNewUser(email, createUserRequest);
            return Ok();
        }
    }
}
