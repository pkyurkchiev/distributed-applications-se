using Greet;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace GrpcGreeter.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);

            var channel = new Channel("localhost:5051", ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);

            await channel.ShutdownAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
