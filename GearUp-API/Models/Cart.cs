namespace GearUp_API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        // Foreign key to Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public void AddItem(int productId, int quantity, decimal unitPrice)
        {
            var existingCartItem = CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (existingCartItem != null)
            {
                // If item already exists in the cart, update the quantity
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Add a new item to the cart
                CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    CustomerId = CustomerId
                });
            }
        }

        public void UpdateItem(int productId, int quantity)
        {
            var existingCartItem = CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity = quantity;
            }
        }

        public void RemoveItem(int productId)
        {
            var existingCartItem = CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingCartItem != null)
            {
                CartItems.Remove(existingCartItem);
            }
        }
    }
}
