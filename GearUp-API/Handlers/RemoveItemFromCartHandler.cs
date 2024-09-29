using GearUp_API.Commands;
using GearUp_API.Repositories;
using MediatR;

namespace GearUp_API.Handlers
{
    public class RemoveItemFromCartHandler : IRequestHandler<RemoveItemFromCartCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemFromCartHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            
            var cartItem = await _unitOfWork.CartItems
                .FindAsync(ci => ci.CustomerId == request.CustomerId && ci.ProductId == request.ProductId);

           
            if (cartItem == null)
            {
                throw new KeyNotFoundException("Item not found in the cart.");
            }
           
            _unitOfWork.CartItems.Remove(cartItem);
           
            await _unitOfWork.CompleteAsync();
            
            return Unit.Value;
        }
    }
}
