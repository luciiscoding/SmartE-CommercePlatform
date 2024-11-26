using Application.DTOs.Product;
using Application.UseCases.Product.Commands.CreateProduct;
using AutoMapper;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Xunit;

namespace UnitTests.Commands
{
    public class CreateProductCommandTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public CreateProductCommandTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }


        [Fact]
        public async void CreateProductCommand_ValidCommand_ShouldPass()
        {
            var product = new CreateProductDTO
            ("Type","Product 1", "Description", 10.22, 3);


            // Arrange
            var command = new CreateProductCommand(product);
            var validator = Substitute.For<IValidator<CreateProductCommand>>();
            validator.ValidateAsync(command, CancellationToken.None)
                .Returns(Task.FromResult(new ValidationResult()));
            var expectedGuid = Guid.NewGuid();
            repository.CreateProduct(Arg.Any<Domain.Entities.Product>()).Returns(Task.FromResult(expectedGuid));
            var handler = new CreateProductCommandHandler(repository, mapper, validator);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            Assert.Equal(expectedGuid, result);
        }

        [Fact]
        public async void CreateProductCommand_InvalidCommand_ShouldFail()
        {
            var product = new CreateProductDTO
           ("Type", "Product 1", "Description", -1, 3);

            // Arrange
            var command = new CreateProductCommand(product);
            var validationFailures = new[]
    {
        new ValidationFailure("Price", "Price must be greater than 0.")
    };
            var validator = Substitute.For<IValidator<CreateProductCommand>>();
            validator.ValidateAsync(command, CancellationToken.None)
                .Returns(Task.FromResult(new ValidationResult(validationFailures)));
            var handler = new CreateProductCommandHandler(repository, mapper, validator);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.Null(result);

            await Assert.ThrowsAsync<ValidationException>(
        async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
