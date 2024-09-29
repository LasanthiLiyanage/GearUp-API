namespace GearUp_API.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        ICartRepository Carts { get; }
        IOrderRepository Orders { get; }
        ICartRepository CartItems { get; }
        Task<int> CompleteAsync();
    }
}
