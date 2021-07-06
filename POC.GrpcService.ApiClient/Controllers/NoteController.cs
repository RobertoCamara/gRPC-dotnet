using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POC.GrpcService.ApiClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.GrpcService.ApiClient.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NoteController: ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private readonly INoteServiceClient _noteServiceClient;

        public NoteController(ILogger<NoteController> logger, INoteServiceClient noteServiceClient)
        {
            _logger = logger;
            _noteServiceClient = noteServiceClient;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _noteServiceClient.List();
            return Ok(result);
        }
    }
}
