using GearUp_API.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GearUp_API.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetByCustomerIdAsync(int customerId);
        Task AddItemAsync(int customerId, int productId, int quantity);
        Task UpdateItemQuantityAsync(int cartItemId, int quantity);
        Task RemoveItemAsync(int cartItemId);
        Task<List<CartItem>> GetCartItemsByCustomerIdAsync(int customerId);        
        Task<CartItem> FindAsync(Expression<Func<CartItem, bool>> predicate);
        void Remove(CartItem cartItem);
        void Add(Cart cart);
        Task SaveAsync();
    }
}
