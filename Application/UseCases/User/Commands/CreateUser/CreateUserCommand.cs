using Application.DTOs.User;
using MediatR;

namespace Application.UseCases.User.Commands.CreateUser
{
   
        public class CreateUserCommand : IRequest<Guid>
        {
            public CreateUserDTO User { get; set; }

            public CreateUserCommand(CreateUserDTO user)
            {
                User = user;
            }
        }
    }

