using Domain.Repositories;
using MediatR;

namespace Application.UseCases.User.Commands.DeleteUser
{
    public class DeleteUserComandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserComandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await userRepository.DeleteUser(request.Id);
        }
    }
}
