[![](https://img.shields.io/nuget/v/Soenneker.ServiceBus.Queue.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.ServiceBus.Queue/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.servicebus.queue/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.servicebus.queue/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.ServiceBus.Queue.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.ServiceBus.Queue/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.servicebus.queue/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.servicebus.queue/actions/workflows/codeql.yml)

# Soenneker.ServiceBus.Queue

Azure Service Bus helpers for creating a queue when absent and destructively removing its currently available active messages.

## Installation

```bash
dotnet add package Soenneker.ServiceBus.Queue
```

## Configuration and registration

Set `Azure:ServiceBus:ConnectionString`, then register the utility:

```csharp
using Soenneker.ServiceBus.Queue.Registrars;

services.AddServiceBusQueueUtilAsScoped();
```

The scoped registrar deliberately keeps the underlying administration and data-plane client utilities singleton while making `IServiceBusQueueUtil` scoped. `AddServiceBusQueueUtilAsSingleton()` is also available.

The connection-string credential needs management permission to create queues and receive permission to empty them.

## Ensure a queue exists

```csharp
await queueUtil.CreateQueueIfDoesNotExist(
    "orders",
    cancellationToken);
```

The queue is created with Azure SDK defaults. This method does not update an existing queue or configure settings such as lock duration, duplicate detection, partitioning, maximum delivery count, or dead lettering. Use `ServiceBusAdministrationClient` directly when custom entity options are required.

The existence check and creation are separate broker operations. Concurrent callers can race; coordinate provisioning when more than one instance may create the same entity.

## Empty a queue

```csharp
await queueUtil.EmptyQueue("orders", cancellationToken);
```

`EmptyQueue` is destructive. It receives batches of up to 100 messages in `ReceiveAndDelete` mode and stops after a one-second receive returns no messages. Received messages cannot be recovered or abandoned.

This operation drains currently available active messages from the queue's primary entity. It does not purge the dead-letter subqueue, explicitly receive deferred messages, cancel scheduled messages, or prevent producers from adding messages concurrently. A producer can enqueue a message after the final empty receive.

Do not use `EmptyQueue` as part of normal message processing. It is intended for controlled cleanup where permanent deletion is acceptable.
