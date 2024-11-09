using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return userRepository.CreateUser(mapper.Map<Domain.Entities.User>(request.User));
        }
    }
}
