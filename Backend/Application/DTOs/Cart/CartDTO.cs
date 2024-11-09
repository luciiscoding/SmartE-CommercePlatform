using Application.DTOs.Product;
using Application.DTOs.User;

namespace Application.DTOs.Cart
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public List<ProductDTO> Products { get; set; }
        public UserDTO User { get; set; }

        public CartDTO()
        {
        }

        public CartDTO(Guid id, List<ProductDTO> products, UserDTO user)
        {
            Id = id;
            Products = products;
            User = user;
        }
    }
}
