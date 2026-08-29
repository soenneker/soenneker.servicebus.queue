using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.ServiceBus.Queue.Abstract;

/// <summary>
/// A utility library for Azure Service Bus queue accessibility <para/>
/// Singleton IoC
/// </summary>
public interface IServiceBusQueueUtil
{
    /// <summary>
    /// Creates queue if does not exist.
    /// </summary>
    /// <param name="queue">Queue for the create queue if does not exist operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the queue if does not exist creation is complete.</returns>
    ValueTask CreateQueueIfDoesNotExist(string queue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the value produced by empty Queue.
    /// </summary>
    /// <param name="queue">Queue for the empty queue operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the empty queue operation is complete.</returns>
    ValueTask EmptyQueue(string queue, CancellationToken cancellationToken = default);
}
