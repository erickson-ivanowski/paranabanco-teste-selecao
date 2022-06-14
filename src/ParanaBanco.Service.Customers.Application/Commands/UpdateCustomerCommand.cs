using MediatR;

namespace ParanaBanco.Service.Customers.Application.Commands
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string NewEmail { get; set; }
        public string NewFullName { get; set; }
    }
}
