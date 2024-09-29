using GearUp_API.Queries;
using GearUp_API.Repositories;
using MediatR;

namespace GearUp_API.Handlers
{
    public class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, List<CartItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCartItemsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CartItemDto>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
           
            var cartItems = await _unitOfWork.CartItems.GetCartItemsByCustomerIdAsync(request.CustomerId);
            
            var cartItemDtos = cartItems.Select(ci => new CartItemDto
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name, 
                Price = ci.UnitPrice, 
                Quantity = ci.Quantity
            }).ToList();

            return cartItemDtos;
        }
    }
}
