using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityAttendee> ActivityAttendees {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ActivityAttendee>(x=>x.HasKey(aa=> new {aa.AppUserId , aa.ActivityId})); //Setup Primary key that combine activity and user in table
            builder.Entity<ActivityAttendee>()
                .HasOne(u=> u.AppUser).WithMany(a=>a.Activities).HasForeignKey(aa=>aa.AppUserId); //Configure entity for app user
            builder.Entity<ActivityAttendee>()
                .HasOne(u=>u.Activity).WithMany(a=>a.Attendees).HasForeignKey(aa=>aa.ActivityId); //Configure entity for activity
        } //This code for configuration many to many relationship
    }
}