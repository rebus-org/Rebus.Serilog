using System;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Transport.InMem;
using Serilog;
#pragma warning disable CS1998

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} {Level:u3} ({CorrelationId}) {Message}{NewLine}{Exception}")
    .Enrich.WithRebusCorrelationId("CorrelationId")
    .CreateLogger();

using var activator = new BuiltinHandlerActivator();

activator.Handle<string>(async message => Log.Information("Received message: {message}", message));

var bus = Configure.With(activator)
    .Logging(l => l.Serilog(Log.Logger))
    .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "logging-test"))
    .Start();

await bus.SendLocal("Hello there, this is the first message");

await bus.SendLocal("Hello there, this is the second message");

Console.WriteLine("Press ENTER to quit");
Console.ReadLine();