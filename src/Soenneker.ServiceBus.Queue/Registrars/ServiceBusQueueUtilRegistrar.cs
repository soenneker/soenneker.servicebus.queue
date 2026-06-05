using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.ServiceBus.Admin.Registrars;
using Soenneker.ServiceBus.Client.Registrars;
using Soenneker.ServiceBus.Queue.Abstract;

namespace Soenneker.ServiceBus.Queue.Registrars;

/// <summary>
/// A utility library for Azure Service Bus queue accessibility
/// </summary>
public static class ServiceBusQueueUtilRegistrar
{
    /// <summary>
    /// Adds service bus queue util as singleton.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
    public static IServiceCollection AddServiceBusQueueUtilAsSingleton(this IServiceCollection services)
    {
        services.AddServiceBusAdminUtilAsSingleton()
                .AddServiceBusClientUtilAsSingleton()
                .TryAddSingleton<IServiceBusQueueUtil, ServiceBusQueueUtil>();

        return services;
    }

    /// <summary>
    /// Adds service bus queue util as scoped.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
    public static IServiceCollection AddServiceBusQueueUtilAsScoped(this IServiceCollection services)
    {
        services.AddServiceBusAdminUtilAsSingleton()
                .AddServiceBusClientUtilAsSingleton()
                .TryAddScoped<IServiceBusQueueUtil, ServiceBusQueueUtil>();

        return services;
    }
}