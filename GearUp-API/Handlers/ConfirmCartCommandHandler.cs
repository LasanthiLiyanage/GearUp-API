using GearUp_API.Commands;
using GearUp_API.Data;
using GearUp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace GearUp_API.Handlers
{
    public class ConfirmCartCommandHandler : IRequestHandler<ConfirmCartCommand, bool>
    {
        private readonly GearUpDbContext _context;
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<ConfirmCartCommandHandler> _logger;

        public ConfirmCartCommandHandler(GearUpDbContext context,  IOptions<EmailSettings> emailSettings, ILogger<ConfirmCartCommandHandler> logger)
        {
            _context = context;
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> Handle(ConfirmCartCommand request, CancellationToken cancellationToken)
        {
            try
            {               
                await SaveOrderAsync(request);                
                SendConfirmationEmail(request.Email, request.CartItems);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while confirming the cart for email: {Email}", request.Email);
                return false;
            }
        }

        private async Task SaveOrderAsync(ConfirmCartCommand request)
        {            
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == request.Email);

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
         
            decimal totalAmount = request.CartItems.Sum(item => item.Quantity * item.Price);
            
            var order = new Order
            {
                CustomerId = customer.Id,
                TotalAmount = totalAmount,
                OrderDate = DateTime.UtcNow
            };
            
            foreach (var cartItem in request.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Price,                    
                    Order = order
                };

                order.OrderItems.Add(orderItem);
            }            
            _context.Orders.Add(order);
            
            await _context.SaveChangesAsync();
        }

        private void SendConfirmationEmail(string email, List<CartItemDto> cartItems)
        {
            try
            {
                string body = "Thank you for your purchase! Here are the items you ordered:\n\n";
                foreach (var item in cartItems)
                {
                    body += $"Product ID: {item.ProductId}, Quantity: {item.Quantity}, Total Price: {item.Price * item.Quantity}\n";
                }

                using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    smtpClient.Port = _emailSettings.Port;
                    smtpClient.EnableSsl = _emailSettings.EnableSsl;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.From),
                        Subject = "Order Confirmation",
                        Body = body,
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(email);

                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send confirmation email to: {Email}", email);
                throw;
            }           
            
        }
    }
}
