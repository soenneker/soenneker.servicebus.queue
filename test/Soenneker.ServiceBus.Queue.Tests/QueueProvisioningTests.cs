using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Logging.Abstractions;
using Soenneker.ServiceBus.Admin.Abstract;

namespace Soenneker.ServiceBus.Queue.Tests;

public class QueueProvisioningTests
{
    [Test]
    [Arguments(true, true)]
    [Arguments(true, false)]
    [Arguments(false, true)]
    public async Task ConcurrentCreationOnlySucceedsWhenTheQueueExists(bool entityExistsError, bool queueExistsAfterRace)
    {
        var admin = new RacingAdmin(entityExistsError, queueExistsAfterRace);
        var util = new ServiceBusQueueUtil(NullLogger<ServiceBusQueueUtil>.Instance, null!, new AdminUtil(admin));
        bool failed = false;
        try { await util.CreateQueueIfDoesNotExist("audit"); }
        catch (ServiceBusException) { failed = true; }
        if (failed == (entityExistsError && queueExistsAfterRace))
            throw new InvalidOperationException("Queue provisioning swallowed an error or failed a successful race");
        if (admin.Checks != (entityExistsError ? 2 : 1))
            throw new InvalidOperationException("Unexpected number of existence checks");
    }

    private sealed class AdminUtil(RacingAdmin admin) : IServiceBusAdminUtil
    {
        public ValueTask<ServiceBusAdministrationClient> Get(CancellationToken cancellationToken = default) =>
            ValueTask.FromResult<ServiceBusAdministrationClient>(admin);
        public void Dispose() { }
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    private sealed class RacingAdmin(bool entityExistsError, bool queueExistsAfterRace) : ServiceBusAdministrationClient
    {
        public int Checks;
        public override Task<Response<bool>> QueueExistsAsync(string name, CancellationToken cancellationToken = default) =>
            Task.FromResult<Response<bool>>(new ExistsResponse(++Checks > 1 && queueExistsAfterRace));

        public override Task<Response<QueueProperties>> CreateQueueAsync(string name, CancellationToken cancellationToken = default) =>
            throw new ServiceBusException("simulated create race", entityExistsError ? ServiceBusFailureReason.MessagingEntityAlreadyExists : ServiceBusFailureReason.ServiceBusy);
    }

    private sealed class ExistsResponse(bool value) : Response<bool>
    {
        public override bool Value => value;
        public override Response GetRawResponse() => throw new NotSupportedException();
    }
}
