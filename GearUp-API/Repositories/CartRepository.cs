using GearUp_API.Data;
using GearUp_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace GearUp_API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly GearUpDbContext _context;

        public CartRepository(GearUpDbContext context)
        {
            _context = context;
        }      
        public async Task<Cart> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }
      
        public async Task AddItemAsync(int customerId, int productId, int quantity)
        {
            var cart = await GetByCustomerIdAsync(customerId);

            if (cart == null)
            {              
                cart = new Cart
                {
                    CustomerId = customerId,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null)
            {                
                existingItem.Quantity += quantity;
            }
            else
            {               
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                cart.CartItems.Add(cartItem);
            }
        }
      
        public async Task UpdateItemQuantityAsync(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.CartItems.Update(cartItem);
            }
        }
      
        public async Task RemoveItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
        }

        public async Task<List<CartItem>> GetCartItemsByCustomerIdAsync(int customerId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product) 
                .Where(ci => ci.CustomerId == customerId)
                .ToListAsync();
        }

        public void Add(Cart cart)
        {
            _context.Carts.Add(cart);  
        }

        public async Task<CartItem> FindAsync(Expression<Func<CartItem, bool>> predicate)
        {
            return await _context.CartItems.FirstOrDefaultAsync(predicate);
        }

        public void Remove(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
