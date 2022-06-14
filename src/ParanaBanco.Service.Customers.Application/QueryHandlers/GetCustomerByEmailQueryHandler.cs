using MediatR;
using ParanaBanco.Service.Customers.Application.Mapping;
using ParanaBanco.Service.Customers.Application.Query;
using ParanaBanco.Service.Customers.Application.ViewModels;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using Serilog;

namespace ParanaBanco.Service.Customers.Application.QueryHandlers
{
    public class GetCustomerByEmailQueryHandler : IRequestHandler<GetCustomerByEmailQuery, CustomerViewModel>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger _log;

        public GetCustomerByEmailQueryHandler(ICustomerRepository customerRepository, ILogger log)
        {
            _customerRepository = customerRepository;
            _log = log;
        }

        public async Task<CustomerViewModel> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Information("Handling {Handle}", nameof(GetCustomerByEmailQuery));

                if (await _customerRepository.GetCustomerAsync(request.Email) is var customer && customer is null)
                {
                    _log.Information("Handling {Handle} Customer {Email} not found", nameof(GetCustomerByEmailQuery), request.Email);
                    return null;
                }

                _log.Information("Handling {Handle} Customer {Email} found", nameof(GetCustomerByEmailQuery), request.Email);
                return customer.AsViewModel();
            }
            catch (Exception ex)
            {
                _log.Error("Handling {Handle} Error: {ErrorMessage}", nameof(GetCustomerByEmailQuery), ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Information("Handled {Handle}", nameof(GetCustomerByEmailQuery));
            }
        }
    }
}
