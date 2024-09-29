using GearUp_API.Data;
using GearUp_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GearUp_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly GearUpDbContext _context;

        public ProductRepository(GearUpDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
