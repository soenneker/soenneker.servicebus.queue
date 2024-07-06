using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Logging;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using Soenneker.ServiceBus.Admin.Abstract;
using Soenneker.ServiceBus.Client.Abstract;
using Soenneker.ServiceBus.Queue.Abstract;

namespace Soenneker.ServiceBus.Queue;

///<inheritdoc cref="IServiceBusQueueUtil"/>
public class ServiceBusQueueUtil : IServiceBusQueueUtil
{
    private readonly ILogger<ServiceBusQueueUtil> _logger;
    private readonly IServiceBusClientUtil _serviceBusClientUtil;
    private readonly IServiceBusAdminUtil _serviceBusAdminUtil;

    public ServiceBusQueueUtil(ILogger<ServiceBusQueueUtil> logger, IServiceBusClientUtil serviceBusClientUtil, IServiceBusAdminUtil serviceBusAdminUtil)
    {
        _logger = logger;
        _serviceBusClientUtil = serviceBusClientUtil;
        _serviceBusAdminUtil = serviceBusAdminUtil;
    }

    public async ValueTask CreateQueueIfDoesNotExist(string queue, CancellationToken cancellationToken = default)
    {
        ServiceBusAdministrationClient adminClient = await _serviceBusAdminUtil.Get(cancellationToken).NoSync();

        Response<bool>? queueExists = await adminClient.QueueExistsAsync(queue, cancellationToken).NoSync();

        if (queueExists is not {Value: true})
        {
            _logger.LogInformation("== SERVICEBUSQUEUEUTIL: Queue did not exist, creating: {queue} ...", queue);

            await adminClient.CreateQueueAsync(queue, cancellationToken).NoSync();

            _logger.LogInformation("== SERVICEBUSQUEUEUTIL: Queue finished creating: {queue}", queue);
        }
    }

    public async ValueTask EmptyQueue(string queue, CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("== SERVICEBUSQUEUEUTIL: Emptying queue {queue} ...", queue);

        var receiverOptions = new ServiceBusReceiverOptions
        {
            ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
        };

        ServiceBusClient client = await _serviceBusClientUtil.Get(cancellationToken).NoSync();

        ServiceBusReceiver? receiver = client.CreateReceiver(queue, receiverOptions);

        while (await receiver.PeekMessageAsync(cancellationToken: cancellationToken).NoSync() != null)
        {
            _ = await receiver.ReceiveMessagesAsync(100, cancellationToken: cancellationToken).NoSync();
        }

        await receiver.DisposeAsync().NoSync();

        _logger.LogInformation("== SERVICEBUSQUEUEUTIL: Finished clearing queue {queue}", queue);
    }
}