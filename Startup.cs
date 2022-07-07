using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shm.Logging;
using Shm.Services.Data;
using Shm.Services.Functions;
using Shm.Servicing.Data;
using Shm.Servicing.Functions;
using Shm.Servicing.Handlers;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Shm.Servicing.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddJwtAuthentication();
            builder.Services.AddApplicationLogger();

            builder.Services.AddSingleton<IConnectionFactory>(s => new SqlConnectionFactory(s.GetService<IConfiguration>()));
            builder.Services.AddSingleton<IRetryPolicyFactory, ServiceRetryFactory>();
            builder.Services.AddScoped<IServiceDataManager, ServiceDataManager>();
            builder.Services.AddScoped<IFunctionHandler, FunctionHandler>();
            builder.Services.AddScoped<IAuditEntityManager, AuditEntityManager>();
           

            builder.Services.AddMediatR(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
            builder.Services.AddScoped<IWorkOrderDataManager, WorkOrderDataManager>();

            builder.Services.AddAutoMapper(
                typeof(Handlers.AutoMapperProfile).GetTypeInfo().Assembly);
        }
    }
}
