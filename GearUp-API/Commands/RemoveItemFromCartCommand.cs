using MediatR;

namespace GearUp_API.Commands
{
    public class RemoveItemFromCartCommand : IRequest<Unit>
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public RemoveItemFromCartCommand(int customerId, int productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}
