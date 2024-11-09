using Application.DTOs.User;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserById(request.Id);
            return mapper.Map<UserDTO>(user);
        }
    }
}
