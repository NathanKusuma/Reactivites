using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Results<List<ActivityDto>>>{} //Untuk menampung data dan interface nya

        public class Handler : IRequestHandler<Query, Results<List<ActivityDto>>> //Untuk mengatur data n connection nya
        {
        private readonly IMapper _mapper;

        private readonly DataContext _context;
            public Handler(DataContext context,IMapper mapper)
            {
            _mapper = mapper;
            _context = context;
                
            }

            public async Task<Results<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                 var activities = await _context.Activities
                 .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

                 return Results<List<ActivityDto>>.Success(activities);
            }
        }
    }
}