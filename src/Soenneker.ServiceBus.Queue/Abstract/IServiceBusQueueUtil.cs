using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.ServiceBus.Queue.Abstract;

/// <summary>
/// A utility library for Azure Service Bus queue accessibility <para/>
/// Singleton IoC
/// </summary>
public interface IServiceBusQueueUtil
{
    ValueTask CreateQueueIfDoesNotExist(string queue, CancellationToken cancellationToken = default);

    ValueTask EmptyQueue(string queue, CancellationToken cancellationToken = default);
}