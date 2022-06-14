using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ParanaBanco.Service.Customers.Application.Commands;
using ParanaBanco.Service.Customers.Application.Core;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using ParanaBanco.Service.Customers.Domain.Interfaces.Services;
using ParanaBanco.Service.Customers.Domain.Services;
using ParanaBanco.Service.Customers.Infrastructure.Data.Config;
using ParanaBanco.Service.Customers.Infrastructure.Data.Repositories;
using Serilog;
using Serilog.Events;
using System.Reflection;

namespace ParanaBanco.Service.Customers.Api.Core.IoC
{
    public static class DependencyInversion
    {
        public static void RegisterDependencies(this WebApplicationBuilder builder)
        {
            // Configuration file
            var configuration = builder.Configuration;

            // Core
            builder.Services.AddControllers();

            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Paraná Banco - API",
                    Description = "Este é um teste para entrar no Paraná Banco ",
                    Contact = new OpenApiContact()
                    {
                        Name = "Erickson Ivanowski",
                        Email = "cwberick@live.nl"
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            // Mediator
            builder.Services.AddMediatR(typeof(CreateCustomerCommand));

            // Database
            builder.Services.AddDbContext<CustomersDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("CustomersDbContext")));

            // Repositories
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Domain Services
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            // Notification
            builder.Services.AddScoped<INotificationContext, NotificationContext>();
            builder.Services.AddMvc(options => options.Filters.Add<NotificationFilter>());

            // Domain Services
            builder.Services.AddScoped<CustomerService>();

            // Serilog
            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console(LogEventLevel.Debug));
        }
    }
}
