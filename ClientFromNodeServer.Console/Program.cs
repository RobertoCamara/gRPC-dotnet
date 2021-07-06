using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace ClientFromNodeServer.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://localhost:50051");
            var client = new NoteService.NoteServiceClient(channel);

            var reply = await client.ListAsync(new Void());

            foreach (var note in reply.Notes)
            {
                System.Console.WriteLine($"Id: {note.Id} - Title: {note.Title} - Description: {note.Description}");
            }
            
            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}
