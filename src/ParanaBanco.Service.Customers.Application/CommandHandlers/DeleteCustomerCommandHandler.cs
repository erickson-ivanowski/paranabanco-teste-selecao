using MediatR;
using ParanaBanco.Service.Customer.Api.Endpoints.Core;
using ParanaBanco.Service.Customers.Application.Commands;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using ParanaBanco.Service.Customers.Domain.Notifications;
using Serilog;

namespace ParanaBanco.Service.Customers.Application.CommandHandlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger _log;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, INotificationContext notificationContext, ILogger log)
        {
            _customerRepository = customerRepository;
            _notificationContext = notificationContext;
            _log = log;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Information("Handling {Handle}", nameof(DeleteCustomerCommand));

                if (await _customerRepository.GetCustomerAsync(request.Email) is var customer && customer is null)
                {
                    _notificationContext.AddNotification(new CustomerNotFoundNotification());
                    _log.Information("Handling {Handle} Customer {Email} not found", nameof(DeleteCustomerCommand), request.Email);
                    return Unit.Value;
                }

                await _customerRepository.DeleteAsync(customer);
                _log.Information("Handling {Handle} Customer {Email} is deleted", nameof(DeleteCustomerCommand), request.Email);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _log.Error("Handling {Handle} Error: {ErrorMessage}", nameof(DeleteCustomerCommand), ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Information("Handled {Handle}", nameof(DeleteCustomerCommand));
            }
        }
    }
}
