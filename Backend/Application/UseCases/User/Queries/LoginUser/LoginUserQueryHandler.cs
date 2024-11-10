using Application.DTOs.User;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.User.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, UserDTO>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public LoginUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserDTO> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<UserDTO>(await userRepository.LoginUser(mapper.Map<Domain.Entities.User>(request.User)));
        }

    }
}
