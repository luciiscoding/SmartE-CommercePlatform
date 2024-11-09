using Application.DTOs.User;
using MediatR;

namespace Application.UseCases.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDTO>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
