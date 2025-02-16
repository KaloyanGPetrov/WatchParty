using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WatchParty.Data.Enteties;

namespace WatchParty.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MovieActor>().HasKey(m => new { m.MovieID, m.ActorID });

            builder.Entity<MovieActor>().HasOne(m => m.Movie)
                .WithMany(mo => mo.MovieActors)
                .HasForeignKey(m => m.MovieID);

            builder.Entity<MovieActor>().HasOne(m => m.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(m => m.ActorID);


            builder.Entity<MovieCategory>().HasKey(m => new { m.MovieID, m.CategoryID });

            builder.Entity<MovieCategory>().HasOne(m => m.Movie)
                .WithMany(mo => mo.MovieCategories)
                .HasForeignKey(m => m.MovieID);

            builder.Entity<MovieCategory>().HasOne(m => m.Category)
                .WithMany(c => c.MovieCategories)
                .HasForeignKey(m => m.CategoryID);
        }
        public DbSet<WatchParty.Data.Enteties.Actor> Actor { get; set; } = default!;
        public DbSet<WatchParty.Data.Enteties.Category> Category { get; set; } = default!;
        public DbSet<WatchParty.Data.Enteties.Movie> Movie { get; set; } = default!;


    }
}
