using Soenneker.ServiceBus.Queue.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.ServiceBus.Queue.Tests;

[Collection("Collection")]
public class ServiceBusQueueUtilTests : FixturedUnitTest
{
    private readonly IServiceBusQueueUtil _util;

    public ServiceBusQueueUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IServiceBusQueueUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
