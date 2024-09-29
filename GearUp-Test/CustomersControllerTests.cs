using GearUp_API.Commands;
using GearUp_API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace GearUp_Test
{
    public class CustomersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<CustomersController>> _loggerMock;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<CustomersController>>();
            _controller = new CustomersController(_mediatorMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsOk_WithValidData()
        {
         
            var command = new CreateCustomerCommand
            {
                Name = "Lasanthi Liyanage",
                Email = "lasanthi@gmail.com",
                Address = "Kottawa sri Lanka"
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), default))
                .ReturnsAsync(1); 
          
            var result = await _controller.CreateCustomer(command);
          
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal(1, response.Id);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateCustomerCommand>(), default), Times.Once);
        }
    }
}