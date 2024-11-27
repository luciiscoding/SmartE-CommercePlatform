using Application.DTOs.User;
using Application.UseCases.User.Queries.GetUserById;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace UnitTests.Queries
{
    public class GetUserByIdQueryTests
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUserByIdQueryTests()
        {
            userRepository = Substitute.For<IUserRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async void GetUserByIdQuery_ShouldReturnUser()
        {
            var user = GenerateUser();
            userRepository.GetUserById(user.Id).Returns(user);

            var query = new GetUserByIdQuery(user.Id);
            GenerateUserDTO(user);
            var handler = new GetUserByIdQueryHandler(userRepository, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);

            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            result.Username.Should().Be(user.Username);
            result.Email.Should().Be(user.Email);
        }

        [Fact]
        public async void GetUserByIdQuery_InvalidId_ShouldReturnNull()
        {
            userRepository.GetUserById(Arg.Any<Guid>()).Returns((User)null);

            var query = new GetUserByIdQuery(Guid.NewGuid());
            var handler = new GetUserByIdQueryHandler(userRepository, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        private void GenerateUserDTO(User user)
        {
            mapper.Map<UserDTO>(Arg.Any<User>()).Returns(new UserDTO { Id = user.Id, Username = user.Username, Email = user.Email });
        }

        private User GenerateUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = "Username",
                Password = "password",
                Email = "email"
            };
        }
    }
}
