using Application.DTOs.Product;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Product.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<PagedResult<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllProducts();

            if (!string.IsNullOrEmpty(request.Type))
                products = products.Where(p => p.Type == request.Type);

            if (request.MinPrice.HasValue)
                products = products.Where(p => p.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                products = products.Where(p => p.Price <= request.MaxPrice.Value);

            if (request.MinReview.HasValue)
                products = products.Where(p => p.Review >= request.MinReview.Value);

            int totalItems = products.Count();

            int skip = (request.PageNumber ) * request.PageSize;
            products = products.Skip(skip).Take(request.PageSize);

            var productDTOs = mapper.Map<IEnumerable<ProductDTO>>(products);

            return new PagedResult<ProductDTO>(productDTOs, totalItems, request.PageNumber, request.PageSize);
        }

    }

}
