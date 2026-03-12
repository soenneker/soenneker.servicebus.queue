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
    public static IServiceCollection AddServiceBusQueueUtilAsSingleton(this IServiceCollection services)
    {
        services.AddServiceBusAdminUtilAsSingleton()
                .AddServiceBusClientUtilAsSingleton()
                .TryAddSingleton<IServiceBusQueueUtil, ServiceBusQueueUtil>();

        return services;
    }

    public static IServiceCollection AddServiceBusQueueUtilAsScoped(this IServiceCollection services)
    {
        services.AddServiceBusAdminUtilAsSingleton()
                .AddServiceBusClientUtilAsSingleton()
                .TryAddScoped<IServiceBusQueueUtil, ServiceBusQueueUtil>();

        return services;
    }
}