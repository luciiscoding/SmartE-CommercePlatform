namespace Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Cart()
        {
        }

        public Cart(Guid id)
        {
            Id = id;
        }
    }
}
