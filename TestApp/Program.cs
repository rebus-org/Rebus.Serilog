using System;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Transport.InMem;
using Serilog;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            using (var activator = new BuiltinHandlerActivator())
            {
                Configure.With(activator)
                    .Logging(l => l.Serilog(Log.Logger))
                    .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "logging-test"))
                    .Start();

                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
            }
        }
    }
}
