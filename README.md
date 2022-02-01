# Rebus.Serilog

[![install from nuget](https://img.shields.io/nuget/v/Rebus.Serilog.svg?style=flat-square)](https://www.nuget.org/packages/Rebus.Serilog)

Provides a Serilog logging integration for [Rebus](https://github.com/rebus-org/Rebus).

![](https://raw.githubusercontent.com/rebus-org/Rebus/master/artwork/little_rebusbus2_copy-200x200.png)

---

Do it like this if you just want Rebus to use your global Serilog logger directly:

```csharp
Configure.With(...)
	.Logging(l => l.Serilog())
	.Transport(t => t.Use(...))
	.(...)
	.Start();
```

or like this if you want to customize it:

```csharp
var logger = Log.ForContext("queue", queueName);

Configure.With(...)
	.Logging(l => l.Serilog(logger))
	.Transport(t => t.Use(..., queueName))
	.(...)
	.Start();
```

Initialize your Serilog logging with Rebus' correlation ID enricher if you'd like the correlation ID of handled messages to be added automatically to all log output generated from message handlers:

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.(...)
    .Enrich.WithRebusCorrelationId("CorrelationId")
    .CreateLogger();
```

and then either use real structured logging (e.g. to an aggregator like Elastic), or remember to include it in your output template (here shown with `ColoredConsole`):

```csharp
//                                                                                  👇
Log.Logger = new LoggerConfiguration()
	.WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm:ss} {Level:u3} ({CorrelationId}) {Message}{NewLine}{Exception}")
	.(...)
```