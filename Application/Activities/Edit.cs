using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest<Results<Unit>>
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>{
            public CommandValidator(){
                RuleFor(x=>x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command,Results<Unit>>
        {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
            public Handler(DataContext context,IMapper mapper)
            {
            _mapper = mapper;
            _context = context;
            }

            public async Task<Results<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
             var activity = await _context.Activities.FindAsync(request.Activity.Id);//Untuk get database

             if(activity == null) return null;
            
            //cara banyak item
            _mapper.Map(request.Activity,activity);//auto mapper akan mengambil dari Activity dan akan mengupdate pada activity

            //Cara per item
            //  activity.Title = request.Activity.Title ?? activity.Title;
            //  //?? diberikan apabila title tidak di update sehingga content db tetap sama
            
             var result=await _context.SaveChangesAsync() > 0;
             if(!result) return Results<Unit>.Failure("Failed to update Activity");

             return Results<Unit>.Success(Unit.Value);
            }
        }
    }

}