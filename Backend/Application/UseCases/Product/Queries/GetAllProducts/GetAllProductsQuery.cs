using Application.DTOs.Product;
using MediatR;

namespace Application.UseCases.Product.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
        public GetAllProductsQuery() { }
    }
}
