using GearUp_API.Commands;
using GearUp_API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GearUp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IMediator mediator, ILogger<CustomersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            try
            {
                if (command == null)
                {
                    _logger.LogWarning("CreateCustomer called with null command.");
                    return BadRequest("Invalid customer data.");
                }

                var customerId = await _mediator.Send(command);

                if (customerId <= 0)
                {
                    _logger.LogError("Failed to create customer. CustomerId: {customerId}", customerId);
                    return StatusCode(500, "Failed to create customer.");
                }

                _logger.LogInformation("Customer created successfully with ID: {customerId}", customerId);
                return Ok(new { Id = customerId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a customer.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{customerId}/email")]
        public async Task<IActionResult> GetCustomerEmail(int customerId)
        {
            try
            {
                if (customerId <= 0)
                {                   
                    return BadRequest("Invalid customer ID.");
                }

                var query = new GetCustomerEmailQuery(customerId);
                var email = await _mediator.Send(query);

                if (string.IsNullOrEmpty(email))
                {                   
                    return NotFound("Customer email not found.");
                }

                _logger.LogInformation("Customer email retrieved for customerId: {customerId}", customerId);
                return Ok(new { Email = email });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving customer email for customerId: {customerId}", customerId);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
