using GearUp_API.Data;
using GearUp_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GearUp_API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly GearUpDbContext _context;

        public CustomerRepository(GearUpDbContext context)
        {
            _context = context;
        }
        
        public async Task<Customer> GetByIdAsync(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }
        
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }
      
        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

         public Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return Task.CompletedTask;
        }
        
        public async Task DeleteAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
        }
       
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
