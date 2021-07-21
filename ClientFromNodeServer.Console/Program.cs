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
            using var channel = GrpcChannel.ForAddress("http://localhost:50051");
            //using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new NoteService.NoteServiceClient(channel);

            var reply = await client.ListAsync(new Google.Protobuf.WellKnownTypes.Empty());

            foreach (var note in reply.Notes)
            {
                System.Console.WriteLine($"Id: {note.Id} - Title: {note.Title} - Description: {note.Description}");
            }


            WriteLine("CONSULTANDO NOTE");

            var result = client.FindAsync(new NoteFindRequest { Id = 20 }).GetAwaiter().GetResult()?.Note;

            if (result != null)
                WriteLine($"Id: {result.Id} - Title: {result.Title} - Description: {result.Description}");
            else
                WriteLine("Not Found");

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }


        private static void WriteLine(string text)
        {
            System.Console.WriteLine(text);
        }
    }
}
