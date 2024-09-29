using MediatR;

namespace GearUp_API.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>> { }
  

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
