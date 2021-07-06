using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.GrpcService.ApiClient.Services
{
    public interface INoteServiceClient
    {
        Task<NoteListResponse> List();
    }

    public class NoteServiceClient : INoteServiceClient
    {
        private readonly ILogger<NoteServiceClient> _logger;
        private readonly NoteService.NoteServiceClient _client;

        public NoteServiceClient(ILogger<NoteServiceClient> logger, NoteService.NoteServiceClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<NoteListResponse> List()
        {
            return await _client.ListAsync(new Void());
        }
    }
}
