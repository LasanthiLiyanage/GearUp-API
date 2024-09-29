﻿namespace GearUp_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
       
        public ICollection<OrderItem> OrderItems { get; set; }
       
        public ICollection<CartItem> CartItems { get; set; }
    }
}
