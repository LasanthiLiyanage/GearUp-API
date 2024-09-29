using GearUp_API.Data;
using GearUp_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GearUp_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly GearUpDbContext _context;

        public OrderRepository(GearUpDbContext context)
        {
            _context = context;
        }
      
        public async Task<Order> GetByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)  
                .ThenInclude(oi => oi.Product)  
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
     
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
               .ToListAsync();
        }
        
        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
       
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
      
        public Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            return Task.CompletedTask;
        }
       
        public async Task DeleteAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }
       
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
