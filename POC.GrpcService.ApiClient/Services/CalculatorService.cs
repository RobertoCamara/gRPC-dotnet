using System.Threading.Tasks;

namespace POC.GrpcService.ApiClient.Services
{
    public interface ICalculatorService
    {
        Task<OperationReply> Operation(OperationRequest request);
    }

    public class CalculatorService : ICalculatorService
    {
        private readonly Calculate.CalculateClient _calculate;

        public CalculatorService(Calculate.CalculateClient calculate)
        {
            _calculate = calculate;
        }

        public async Task<OperationReply> Operation(OperationRequest request)
        {
            var response = await _calculate.OperationAsync(request);

            return response;
        }
    }
}
