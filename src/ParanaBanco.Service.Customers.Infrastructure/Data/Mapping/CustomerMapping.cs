namespace ParanaBanco.Service.Customers.Infrastructure.Data.Mapping
{
    public static class CustomerMapping
    {
        public static Models.Customer AsModel(this Domain.Entities.Customer entity)
        {
            var model = new Models.Customer()
            {
                Id = entity.Id,
                Email = entity.Email ?? string.Empty,
                FullName = entity.FullName ?? string.Empty,
            };

            return model;
        }

        public static Domain.Entities.Customer AsEntity(this Models.Customer dataModel)
        {
            var entity = new Domain.Entities.Customer(dataModel.Id, dataModel.Email, dataModel.FullName);

            return entity;
        }
    }
}
