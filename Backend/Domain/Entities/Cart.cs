using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Review { get; set; }

        public Cart()
        {
        }

        public Cart(Guid id, string type, string name, string description, double price, int review)
        {
            Id = id;
            Type = type;
            Description = description;
            Price = price;
            Review = review;
        }
    }
}
