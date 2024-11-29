using Application.DTOs.Product;
using MediatR;

namespace Application.UseCases.Product.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
        public string? Type { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinReview { get; set; }

        public GetAllProductsQuery(string? Type, decimal? minPrice, decimal? maxPrice, int? minReview ) {
            
            this.Type = Type;
            this.MinPrice = minPrice;
            this.MaxPrice = maxPrice;
            this.MinReview = minReview;

        }
    }
}
