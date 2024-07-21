using BuildingBlocks.Behaviours;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Messaging.MassTransit;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                configuration.AddOpenBehavior(typeof(LoggerBehaviour<,>));
            });
            services.AddMessageBrocker(configuration,Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
