using Microsoft.AspNetCore.Mvc;
using POC.GrpcService.ApiClient.Services;
using System;
using System.Threading.Tasks;

namespace POC.GrpcService.ApiClient.Controllers
{
    public class CalculateController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        public CalculateController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpGet("operation")]
        public async Task<IActionResult> Operation(double num1, double num2, TypeOperation typeOperation)
        {

            var result = await _calculatorService.Operation(new OperationRequest
            {
                Num1 = num1,
                Num2 = num2,
                TypeOperation = (int)typeOperation
            });

            if (string.IsNullOrEmpty(result.Error))
                return await Task.FromResult(Ok(result.Message));

            return BadRequest(result.Error);
        }
    }

    public enum TypeOperation
    {
        Sum = 0,
        Subtraction = 1,
        Multiplication = 2,
        Division = 3,
    }
}
