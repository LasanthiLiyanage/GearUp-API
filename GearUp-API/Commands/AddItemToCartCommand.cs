using MediatR;

namespace GearUp_API.Commands
{
    public class AddItemToCartCommand : IRequest<Unit>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
    }
}
