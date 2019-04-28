using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.AzureStorage
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAzureStorage(this IServiceCollection services, AzureStorageOptions option)
        {
            services.AddScoped<IBlobProvider>(provider => new BlobProvider(option));
            return services;
        }
    }
}