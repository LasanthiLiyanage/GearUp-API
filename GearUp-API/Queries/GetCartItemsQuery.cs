using MediatR;

namespace GearUp_API.Queries
{
    public class GetCartItemsQuery : IRequest<List<CartItemDto>>
    {
        public int CustomerId { get; set; }

        public GetCartItemsQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }

    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
