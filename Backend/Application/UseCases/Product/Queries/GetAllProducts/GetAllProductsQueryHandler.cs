using Application.DTOs.Product;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Product.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetFilteredProducts(request.Type, request.MinPrice, request.MaxPrice, request.MinReview);
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }
    }
}
