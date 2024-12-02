namespace Application.DTOs.Product
{
    public class ProductDTO : CreateProductDTO
    {
        public Guid? Id { get; set; }

        public ProductDTO() : base()
        {
        }

        public ProductDTO(Guid id, string type, string name, string description, decimal price, int review) : base(type, name, description, price, review)
        {
            Id = id;
        }
    }
}
