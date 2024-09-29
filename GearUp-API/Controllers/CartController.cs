using GearUp_API.Commands;
using GearUp_API.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GearUp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CartController> _logger;

        public CartController(IMediator mediator, ILogger<CartController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddItemToCartCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Item added to cart successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding item to cart.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while adding the item to the cart. Please try again later." });
            }
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCartItems(int customerId)
        {
            try
            {
                var cartItems = await _mediator.Send(new GetCartItemsQuery(customerId));
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving cart items for customer ID {customerId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while retrieving the cart items. Please try again later." });
            }
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveItemFromCart([FromBody] RemoveItemFromCartCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing item from cart.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while removing the item from the cart. Please try again later." });
            }
        }

        [HttpPost("update-quantity")]
        public async Task<IActionResult> UpdateItemQuantity([FromBody] UpdateCartItemCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Cart item quantity updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating cart item quantity.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while updating the cart item quantity. Please try again later." });
            }
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmCart([FromBody] ConfirmCartCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { message = "Cart confirmed successfully.", result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while confirming cart.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while confirming the cart. Please try again later." });
            }
        }

        [HttpPost("clear")]
        public async Task<IActionResult> ClearCart([FromBody] ClearCartCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Cart cleared successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while clearing the cart.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while clearing the cart. Please try again later." });
            }
        }
    }
}
