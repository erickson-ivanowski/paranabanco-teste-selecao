using MediatR;
using ParanaBanco.Service.Customers.Application.ViewModels;

namespace ParanaBanco.Service.Customers.Application.Query
{
    public class GetCustomerByEmailQuery : IRequest<CustomerViewModel>
    {
        public string? Email { get; set; }
    }
}
