using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace POC.GrpcService.ApiServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = $"Hello {request.Name} -> {DateTime.UtcNow.ToString("yyyy-MM-dd  HH:mm:ss")}"
            });
        }

        public override Task<HelloReply> GetGuid(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = $"New guid generated: {Guid.NewGuid()}"
            }); ;
        }        
    }    
}
