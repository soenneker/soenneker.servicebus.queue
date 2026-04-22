using Soenneker.ServiceBus.Queue.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.ServiceBus.Queue.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class ServiceBusQueueUtilTests : HostedUnitTest
{
    private readonly IServiceBusQueueUtil _util;

    public ServiceBusQueueUtilTests(Host host) : base(host)
    {
        _util = Resolve<IServiceBusQueueUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
