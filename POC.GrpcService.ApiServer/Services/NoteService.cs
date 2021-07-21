using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.GrpcService.ApiServer.Services
{
    public class NoteNetCoreService : NoteService.NoteServiceBase
    {
        private readonly ILogger<NoteNetCoreService> _logger;
        private static NoteListResponse _noteListResponse;

        public NoteNetCoreService(ILogger<NoteNetCoreService> logger)
        {
            _logger = logger;
            _noteListResponse = GetNotes();
        }

        public override Task<NoteFindResponse> Find(NoteFindRequest request, ServerCallContext context)
        {
            var result = _noteListResponse.Notes.FirstOrDefault(w=> w.Id == request.Id);

            return Task.FromResult(new NoteFindResponse
            {
                Note = result
            });           
        }

        public override Task<NoteListResponse> List(Empty request, ServerCallContext context)
        {
            return Task.FromResult(_noteListResponse);
        }


        private NoteListResponse GetNotes()
        {
            int id = 1;

            var notes = Enumerable.Repeat(new Note(), 10)
                .Select(x => new Note
                {
                    Id = id++,
                    Title = $"Title {id - 1}",
                    Description = $"Description {id - 1}"
                });

            NoteListResponse response = new NoteListResponse();
            response.Notes.AddRange(notes);

            return response;
        }

    }
}
