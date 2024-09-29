using GearUp_API.Commands;
using GearUp_API.Repositories;
using MediatR;

namespace GearUp_API.Handlers
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClearCartCommandHandler> _logger;

        public ClearCartCommandHandler(IUnitOfWork unitOfWork, ILogger<ClearCartCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItems = await _unitOfWork.CartItems.GetCartItemsByCustomerIdAsync(request.CustomerId);

                if (cartItems == null || cartItems.Count == 0)
                {              
                    return Unit.Value;
                }

                foreach (var item in cartItems)
                {
                    _unitOfWork.CartItems.Remove(item);
                }

                await _unitOfWork.CompleteAsync();               

                return Unit.Value;
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex, $"Error occurred while clearing the cart for customer ID {request.CustomerId}.");                
                throw new ApplicationException($"An error occurred while clearing the cart for customer ID {request.CustomerId}.", ex);
            }
        }
    }
}
