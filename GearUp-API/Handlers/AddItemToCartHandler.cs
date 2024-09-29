using GearUp_API.Commands;
using GearUp_API.Models;
using GearUp_API.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GearUp_API.Handlers
{
    public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddItemToCartHandler> _logger;

        public AddItemToCartHandler(IUnitOfWork unitOfWork, ILogger<AddItemToCartHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _unitOfWork.Carts.GetByCustomerIdAsync(request.CustomerId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        CustomerId = request.CustomerId
                    };
                    _unitOfWork.Carts.Add(cart);
                }

                var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);

                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                cart.AddItem(request.ProductId, request.Quantity, product.Price);

                await _unitOfWork.CompleteAsync();

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding item to cart for Customer {CustomerId}.", request.CustomerId);
                throw;  
            }
            

               
        }
    }
}
