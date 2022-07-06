using System;
using Microsoft.Extensions.DependencyInjection;
using Shm.Logging.ApplicationInsights;

namespace Shm.Logging
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds IApplicationLogger as a Scoped service
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationLogger(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped(typeof(IApplicationLogger<>), typeof(ApplicationLogger<>));
        }
    }
}
