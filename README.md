[![](https://img.shields.io/nuget/v/Soenneker.ServiceBus.Queue.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.ServiceBus.Queue/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.servicebus.queue/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.servicebus.queue/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.ServiceBus.Queue.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.ServiceBus.Queue/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.servicebus.queue/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.servicebus.queue/actions/workflows/codeql.yml)

# Soenneker.ServiceBus.Queue

A utility library for Azure Service Bus queue accessibility Singleton IoC.

## Install

```bash
dotnet add package Soenneker.ServiceBus.Queue
```

## Quick start

```csharp
using Soenneker.ServiceBus.Queue.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddServiceBusQueueUtilAsSingleton();
```

Registers Service Bus Queue Util with a singleton lifetime.

## What you get

- `IServiceBusQueueUtil` — A utility library for Azure Service Bus queue accessibility Singleton IoC.
- `ServiceBusQueueUtilRegistrar` — A utility library for Azure Service Bus queue accessibility.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IServiceBusQueueUtil.EmptyQueue(queue, cancellationToken)` | Returns the value produced by empty Queue. | A task that completes when the empty queue operation is complete. |
| `ServiceBusQueueUtilRegistrar.AddServiceBusQueueUtilAsSingleton(services)` | Registers Service Bus Queue Util with a singleton lifetime. | The same service collection, so additional registrations can be chained. |
| `ServiceBusQueueUtilRegistrar.AddServiceBusQueueUtilAsScoped(services)` | Registers Service Bus Queue Util with a scoped lifetime. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
