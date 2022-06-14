using MediatR;
using ParanaBanco.Service.Customers.Application.Commands;
using ParanaBanco.Service.Customers.Application.Core;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using ParanaBanco.Service.Customers.Domain.Notifications;
using ParanaBanco.Service.Customers.Domain.Services;
using Serilog;

namespace ParanaBanco.Service.Customers.Application.CommandHandlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger _log;
        private readonly CustomerService _customerServices;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, INotificationContext notificationContext, ILogger log, CustomerService customerServices)
        {
            _customerRepository = customerRepository;
            _notificationContext = notificationContext;
            _log = log;
            _customerServices = customerServices;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Information("Handling {Handle}", nameof(UpdateCustomerCommand));

                if (await _customerRepository.GetCustomerAsync(request.Email) is var customer && customer is null)
                {
                    _notificationContext.AddNotification(new CustomerNotFoundNotification());
                    _log.Information("Handling {Handle} Customer {Email} not found", nameof(UpdateCustomerCommand), request.Email);
                    return Unit.Value;
                }

                customer.SetDomainService(_customerServices);

                UpdatePropertiesOfCustomer();

                if (await customer.IsValid() is false)
                {
                    _notificationContext.AddNotifications(customer.Notifications);
                    _log.Information("Handling {Handle} Customer {Email} {FullName} is invalid", nameof(UpdateCustomerCommand), customer.Email, customer.FullName);
                    return Unit.Value;
                }

                await _customerRepository.UpdateAsync(customer);
                _log.Information("Handling {Handle} Customer {Email} updated", nameof(UpdateCustomerCommand), customer.Email);

                return Unit.Value;

                void UpdatePropertiesOfCustomer()
                {
                    customer.SetEmail(request.NewEmail ?? customer.Email);
                    customer.SetFullName(request.NewFullName ?? customer.FullName);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Handling {Handle} Error: {ErrorMessage}", nameof(UpdateCustomerCommand), ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Information("Handled {Handle}", nameof(UpdateCustomerCommand));
            }
        }
    }
}
