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
    /// <param name="queue">The queue.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask CreateQueueIfDoesNotExist(string queue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the empty queue operation.
    /// </summary>
    /// <param name="queue">The queue.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask EmptyQueue(string queue, CancellationToken cancellationToken = default);
}