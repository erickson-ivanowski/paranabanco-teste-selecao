using ParanaBanco.Service.Customers.Application.ViewModels;

namespace ParanaBanco.Service.Customers.Application.Mapping
{
    public static class CustomerMapping
    {
        public static CustomerViewModel AsViewModel(this Domain.Entities.Customer entity)
        {
            var viewModel = new CustomerViewModel
            {
                FullName = entity.FullName,
                Email = entity.Email
            };

            return viewModel;
        }
    }
}
