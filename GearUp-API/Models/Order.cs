﻿namespace GearUp_API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
       
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
       
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
