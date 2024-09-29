using GearUp_API.Data;
using GearUp_API.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GearUp_API.Handlers
{
    public class GetCustomerEmailQueryHandler : IRequestHandler<GetCustomerEmailQuery, string>
    {
        private readonly GearUpDbContext _context;

        public GetCustomerEmailQueryHandler(GearUpDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetCustomerEmailQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .Where(c => c.Id == request.CustomerId)
                .Select(c => c.Email)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            return customer;
        }
    }
}
