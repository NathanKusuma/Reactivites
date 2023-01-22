using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Results<List<Activity>>>{} //Untuk menampung data dan interface nya

        public class Handler : IRequestHandler<Query, Results<List<Activity>>> //Untuk mengatur data n connection nya
        {

        private readonly DataContext _context;
            public Handler(DataContext context)
            {
            _context = context;
                
            }

            public async Task<Results<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
            {
                 var activity = await _context.Activities.ToListAsync();
                 return Results<List<Activity>>.Success(activity);
            }
        }
    }
}