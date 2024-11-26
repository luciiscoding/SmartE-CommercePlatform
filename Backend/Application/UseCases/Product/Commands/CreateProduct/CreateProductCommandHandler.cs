using AutoMapper;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateProductCommand> validator;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IValidator<CreateProductCommand> validator)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) 
            {
                throw new ValidationException(validationResult.Errors);
            }

            var product = mapper.Map<Domain.Entities.Product>(request.Product);
            return await productRepository.CreateProduct(product);
        }
    }
}
