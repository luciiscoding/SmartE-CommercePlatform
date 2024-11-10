using Application.DTOs.User;
using MediatR;

namespace Application.UseCases.User.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<UserDTO>
    {
        public LoginUserDTO User { get; set; }

        public LoginUserQuery(LoginUserDTO user)
        {
            User = user;
        }
    }
}
