using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.ServiceBus.Queue.Abstract;

/// <summary>
/// Provides Azure Service Bus queue provisioning and destructive cleanup operations.
/// </summary>
public interface IServiceBusQueueUtil
{
    /// <summary>
    /// Creates the queue with Azure SDK defaults when the entity does not already exist.
    /// </summary>
    /// <param name="queue">Queue for the create queue if does not exist operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the queue if does not exist creation is complete.</returns>
    ValueTask CreateQueueIfDoesNotExist(string queue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Permanently removes currently available active messages in receive-and-delete batches, stopping after a one-second empty receive.
    /// </summary>
    /// <param name="queue">Queue for the empty queue operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when no active message is received during the final wait.</returns>
    ValueTask EmptyQueue(string queue, CancellationToken cancellationToken = default);
}
