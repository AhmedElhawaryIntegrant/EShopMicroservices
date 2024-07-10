﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration )
        {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddDbContext<AbblicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}