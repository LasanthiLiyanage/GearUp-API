using GearUp_API.Queries;
using MediatR;

namespace GearUp_API.Commands
{
    public class ConfirmCartCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
