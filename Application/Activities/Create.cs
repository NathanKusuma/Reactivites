using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command:IRequest //Command tidak mereturn data seperti query
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
               _context.Activities.Add(request.Activity); //code untuk mencari data (tidak dilakukan secara async)
               await _context.SaveChangesAsync();//code untuk save data (dilakukan secara async)
               return Unit.Value;
               //Unit merupakan object kosong, return harus dilakukan karena untuk melanjutkan program harus memberikan return
            }
        }
    }
}