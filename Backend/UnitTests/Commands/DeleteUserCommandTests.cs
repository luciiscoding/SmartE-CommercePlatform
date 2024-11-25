using Application.UseCases.User.Commands.DeleteUser;
using Domain.Repositories;
using NSubstitute;
using Xunit;

namespace UnitTests.Commands
{
    public class DeleteUserCommandTests
    {
        private readonly IUserRepository repository;

        public DeleteUserCommandTests()
        {
            repository = Substitute.For<IUserRepository>();
        }


        [Fact]
        public async void DeleteUserCommand_ValidCommand_ShouldPass()
        {
            // Arrange
            var command = new DeleteUserCommand(new Guid());
            var handler = new DeleteUserCommandHandler(repository);

            // Act
            await handler.Handle(command, CancellationToken.None);
        }

        [Fact]
        public async void DeleteUserCommand_InvalidCommand_ShouldFail()
        {
            // Arrange
            var command = new DeleteUserCommand(new Guid());
            var handler = new DeleteUserCommandHandler(repository);

            // Act
            await handler.Handle(command, CancellationToken.None);
        }
    }
}
