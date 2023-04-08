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
    /// As Singleton
    /// </summary>
    public static void AddServiceBusQueueUtil(this IServiceCollection services)
    {
        services.AddServiceBusAdminUtil();
        services.AddServiceBusClientUtil();
        services.TryAddSingleton<IServiceBusQueueUtil, ServiceBusQueueUtil>();
    }
}