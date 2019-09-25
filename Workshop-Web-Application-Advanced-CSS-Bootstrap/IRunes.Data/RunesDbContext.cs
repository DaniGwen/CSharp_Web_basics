namespace IRunes.Data
{
    using IRunes.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class RunesDbContext : DbContext
    {
        public RunesDbContext()
        {

        }

        public RunesDbContext(DbContextOptions contextOptions):base(contextOptions)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Album> Albums { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ThinkPad-DN\SQLEXPRESS;Database=IRunesDB;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasMany(x => x.Tracks);
            });
        }
    }
}
