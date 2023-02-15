using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Results<ActivityDto>>
        {
           public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Results<ActivityDto>>
        {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
            public Handler(DataContext context,IMapper mapper)
            {
            _mapper = mapper;
            _context = context;
                
            }
            public async Task<Results<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
               var activity= await _context.Activities
               .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(x=>x.Id==request.Id);

                return Results<ActivityDto>.Success(activity);
            }
        }
    }
}