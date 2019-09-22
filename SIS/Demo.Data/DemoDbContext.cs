using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Demo.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext()
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(ConnectionConfig.ConnectionString);

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); 
        }
    }
}
