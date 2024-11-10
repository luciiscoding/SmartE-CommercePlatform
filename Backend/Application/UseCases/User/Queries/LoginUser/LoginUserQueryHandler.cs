using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.User.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public LoginUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.LoginUser(mapper.Map<Domain.Entities.User>(request.User));
        }

    }
}
