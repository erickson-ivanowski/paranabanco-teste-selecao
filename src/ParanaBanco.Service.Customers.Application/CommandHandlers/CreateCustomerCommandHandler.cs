using MediatR;
using ParanaBanco.Service.Customers.Application.Commands;
using ParanaBanco.Service.Customers.Application.Core;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using ParanaBanco.Service.Customers.Domain.Interfaces.Services;
using Serilog;

namespace ParanaBanco.Service.Customers.Application.CommandHandlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly INotificationContext _notificationContext;
        private readonly ILogger _log;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerService customerService, INotificationContext notificationContext, ILogger log)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
            _notificationContext = notificationContext;
            _log = log;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Information("Handling {Handle}", nameof(CreateCustomerCommand));

                var customer = new Domain.Entities.Customer(request.Email, request.FullName);

                if (await IsCustomerValid())
                {
                    _notificationContext.AddNotifications(customer.Notifications);
                    _log.Information("Handling {Handle} Customer Email: {Email} FullName: {FullName} is invalid.", nameof(CreateCustomerCommand), request.Email, request.FullName);
                    return Unit.Value;
                }

                await _customerRepository.SaveAsync(customer);
                _log.Information("Handling {Handle} Customer Email: {Email} is saved.", nameof(CreateCustomerCommand), customer.Email);


                return Unit.Value;

                async Task<bool> IsCustomerValid()
                {
                    return customer.IsValid() is false && await _customerService.CustomerExists(customer) is false;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Handling {Handle} Error: {ErrorMessage}", nameof(CreateCustomerCommand), ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Information("Handled {Handle}", nameof(CreateCustomerCommand));
            }
        }
    }
}
