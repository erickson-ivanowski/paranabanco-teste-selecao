using MediatR;
using ParanaBanco.Service.Customers.Application.Mapping;
using ParanaBanco.Service.Customers.Application.Query;
using ParanaBanco.Service.Customers.Application.ViewModels;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using Serilog;

namespace ParanaBanco.Service.Customers.Application.QueryHandlers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerViewModel>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger _log;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository, ILogger log)
        {
            _customerRepository = customerRepository;
            _log = log;
        }

        public async Task<IEnumerable<CustomerViewModel>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _log.Information("Handling {Handle}", nameof(GetCustomersQuery));

                if (await _customerRepository.GetAllAsync() is var customers && customers.Any() is false)
                {
                    _log.Information("Handling {Handle} No Customers found", nameof(GetCustomersQuery));
                    return Enumerable.Empty<CustomerViewModel>();
                }

                _log.Information("Handling {Handle} {QtdCustomersFound} Customers found", nameof(GetCustomersQuery),customers.Count());
                return customers.Select(x => x.AsViewModel());
            }
            catch (Exception ex)
            {
                _log.Error("Handling {Handle} Error: {ErrorMessage}", nameof(GetCustomersQuery), ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                _log.Information("Handled {Handle}", nameof(GetCustomersQuery));
            }
        }
    }
}
