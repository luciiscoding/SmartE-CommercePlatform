using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await userRepository.UpdateUser(mapper.Map<Domain.Entities.User>(request.User));
        }
    }
}
