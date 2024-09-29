using MediatR;

namespace GearUp_API.Commands
{
    public class ClearCartCommand : IRequest<Unit>
    {
        public int CustomerId { get; set; }

        public ClearCartCommand(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
