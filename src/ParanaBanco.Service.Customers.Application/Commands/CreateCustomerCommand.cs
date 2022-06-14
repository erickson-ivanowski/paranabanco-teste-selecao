using MediatR;

namespace ParanaBanco.Service.Customers.Application.Commands
{
    public class CreateCustomerCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
