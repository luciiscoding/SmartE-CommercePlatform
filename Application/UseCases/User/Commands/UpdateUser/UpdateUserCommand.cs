using Application.DTOs.User;
using MediatR;

namespace Application.UseCases.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public UserDTO User { get; set; }

        public UpdateUserCommand(UserDTO user)
        {
            User = user;
        }
    }
}
