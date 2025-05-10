using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Application
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {

            var assembly = typeof(ApplicationService).Assembly;
            var assembly2 = Assembly.GetExecutingAssembly();

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));
            return services;
        }
    }
}
