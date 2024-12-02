using Application.DTOs.Product;
using MediatR;

namespace Application.UseCases.Product.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<PagedResult<ProductDTO>>
    {
        public string? Type { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinReview { get; set; }
        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 10; 

        public GetAllProductsQuery(
            string? type = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? minReview = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            Type = type;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            MinReview = minReview;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

}
