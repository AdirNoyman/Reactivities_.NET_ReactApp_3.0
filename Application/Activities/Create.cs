using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    // Create an Activity
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }  
        }

        public class Handler : IRequestHandler<Command>
        {
        private readonly DataContext _context;
            public Handler(DataContext context)
            {
            _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // The _context represents our storage in memory only at this stage, so we don't need to use async operation here.
                _context.Activities.Add(request.Activity);

                await _context.SaveChangesAsync();

                // This handler function returns nothing (void). (The Api Controller will return the response to the client)
                return Unit.Value;
            }
        }
    }
}