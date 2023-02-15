using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command:IRequest<Results<Unit>> //Command tidak mereturn data seperti query
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator:AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x=>x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command,Results<Unit>>
        {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
            _userAccessor=userAccessor;
            _context = context;
                
            }

            public async Task<Results<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user= await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername()); //Memberikan access untuk user object

                var attendee= new ActivityAttendee{
                    AppUser=user,
                    Activity=request.Activity,
                    IsHost=true
                }; //Creating new attendee that connected with activity

                request.Activity.Attendees.Add(attendee);

               _context.Activities.Add(request.Activity); //code untuk mencari data (tidak dilakukan secara async)

               var result = await _context.SaveChangesAsync() > 0;//code untuk save data (dilakukan secara async)

               if(!result) Results<Unit>.Failure("Failed To Create Activity");
               return Results<Unit>.Success(Unit.Value);
               //Unit merupakan object kosong, return harus dilakukan karena untuk melanjutkan program harus memberikan return
            }
        }
    }
}