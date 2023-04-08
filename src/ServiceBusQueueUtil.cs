using System.Threading.Tasks;
using Azure;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
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

    public async ValueTask CreateQueueIfDoesNotExist(string queue)
    {
        Response<bool>? queueExists = await (await _serviceBusAdminUtil.GetClient()).QueueExistsAsync(queue);

        if (queueExists == null || !queueExists.Value)
        {
            _logger.LogInformation("== SERVICEBUSQUEUEUTIL: Queue did not exist, creating: {queue} ...", queue);

            await (await _serviceBusAdminUtil.GetClient()).CreateQueueAsync(queue);

            _logger.LogInformation("== SERVICEBUSQUEUEUTIL: Queue finished creating: {queue}", queue);
        }
    }

    public async ValueTask EmptyQueue(string queue)
    {
        _logger.LogWarning("== SERVICEBUSQUEUEUTIL: Emptying queue {queue} ...", queue);

        var receiverOptions = new ServiceBusReceiverOptions
        {
            ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
        };

        ServiceBusClient client = await _serviceBusClientUtil.GetClient();

        ServiceBusReceiver? receiver = client.CreateReceiver(queue, receiverOptions);

        while (await receiver.PeekMessageAsync() != null)
        {
            _ = await receiver.ReceiveMessagesAsync(100);
        }

        await receiver.DisposeAsync();

        _logger.LogInformation("== SERVICEBUSQUEUEUTIL: Finished clearing queue {queue}", queue);
    }
}