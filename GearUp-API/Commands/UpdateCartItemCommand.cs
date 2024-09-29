using MediatR;

namespace GearUp_API.Commands
{
    public class UpdateCartItemCommand : IRequest<Unit>
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }

        public UpdateCartItemCommand(int cartItemId, int quantity)
        {
            CartItemId = cartItemId;
            Quantity = quantity;
        }
    }
}
