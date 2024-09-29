using MediatR;

namespace GearUp_API.Queries
{
    public class GetCustomerEmailQuery : IRequest<string>
    {
        public int CustomerId { get; set; }

        public GetCustomerEmailQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
