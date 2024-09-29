using GearUp_API.Commands;
using GearUp_API.Repositories;
using MediatR;

namespace GearUp_API.Handlers
{
    public class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCartItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        

        public async Task<Unit> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {            
            await _unitOfWork.CartItems.UpdateItemQuantityAsync(request.CartItemId, request.Quantity);
           
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
