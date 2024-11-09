﻿using Application.DTOs.User;
using Application.UseCases.User.Commands.CreateUser;
using Application.UseCases.User.Commands.DeleteUser;
using Application.UseCases.User.Commands.UpdateUser;
using Application.UseCases.User.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SmartE_commercePlatform.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser(CreateUserDTO user)
        {
            var userId = await mediator.Send(new CreateUserCommand(user));
            return Created(nameof(UserDTO), userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            return await mediator.Send(new GetUserByIdQuery(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserDTO user)
        {
            await mediator.Send(new UpdateUserCommand(user));
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }

    }
}
