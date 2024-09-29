using GearUp_API.Data;
using System;

namespace GearUp_API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GearUpDbContext _context;
        private CartRepository _cartRepository;
        public UnitOfWork(GearUpDbContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            Products = new ProductRepository(_context);
            Carts = new CartRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public ICustomerRepository Customers { get; private set; }
        public IProductRepository Products { get; private set; }
        public ICartRepository Carts { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public ICartRepository CartItems
        {
            get
            {
                return _cartRepository ??= new CartRepository(_context);
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
