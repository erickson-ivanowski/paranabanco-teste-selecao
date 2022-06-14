using MediatR;
using ParanaBanco.Service.Customers.Application.ViewModels;

namespace ParanaBanco.Service.Customers.Application.Query
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerViewModel>>
    {
    }
}
