using Carter;
namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services) 
        {
            services.AddCarter();
            return services;
        }

        public static WebApplication UseAPIServices(this WebApplication webApplication)
        {
            webApplication.MapCarter();
            return webApplication;
        }
    }
}
