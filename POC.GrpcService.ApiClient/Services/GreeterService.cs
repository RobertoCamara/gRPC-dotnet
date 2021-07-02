using System.Threading.Tasks;

namespace POC.GrpcService.ApiClient.Services
{
    public interface IGreeterService
    {
        Task<string> SayHelloAsync(HelloRequest request);
        Task<string> GetGuid();
    }

    public class GreeterService : IGreeterService
    {
        private readonly Greeter.GreeterClient _greeterClient;

        public GreeterService(Greeter.GreeterClient greeterClient)
        {
            _greeterClient = greeterClient;
        }

        public async Task<string> GetGuid()
        {
            var response = await _greeterClient.GetGuidAsync(new Google.Protobuf.WellKnownTypes.Empty());

            return response.Message;
        }

        public async Task<string> SayHelloAsync(HelloRequest request)
        {
            var response = await _greeterClient.SayHelloAsync(request);

            return response.Message;
        }
    }
}
