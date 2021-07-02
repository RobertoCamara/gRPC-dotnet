using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.GrpcService.ApiServer.Services
{
    public class CalculateService : Calculate.CalculateBase
    {
        private readonly ILogger<CalculateService> _logger;

        public CalculateService(ILogger<CalculateService> logger)
        {
            _logger = logger;
        }

        public override Task<OperationReply> Operation(OperationRequest request, ServerCallContext context)
        {
            var type = (TypeOperation)Enum.Parse(typeof(TypeOperation), request.TypeOperation.ToString());

            var result = new Calculator().Operation(request.Num1, request.Num2, type);

            return Task.FromResult(new OperationReply
            {
                Message = result.Item1,
                Error = result.Item2
            });
        }
    }

    public enum TypeOperation
    {
        Sum = 0,
        Subtraction = 1,
        Multiplication = 2,
        Division = 3,
    }

    public class Calculator
    {
        public (double, string) Operation(double num1, double num2, TypeOperation op)
        {
            double result = double.NaN;
            string error = string.Empty;

            switch (op)
            {
                case TypeOperation.Sum:
                    result = num1 + num2;
                    break;
                case TypeOperation.Subtraction:
                    result = num1 - num2;
                    break;
                case TypeOperation.Multiplication:
                    result = num1 * num2;
                    break;
                case TypeOperation.Division:
                    if (num2 == 0) error = "Operation fail: Divide By Zero Exception";
                    result = num1 / num2;
                    break;
                default:
                    error = "TypeOperation Invalid";
                    break;

            }
            return (result, error);
        }
    }
}
