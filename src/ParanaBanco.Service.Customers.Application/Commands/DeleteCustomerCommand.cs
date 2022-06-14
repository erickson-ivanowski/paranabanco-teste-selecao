using MediatR;

namespace ParanaBanco.Service.Customers.Application.Commands
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
