using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Demo.Data
{
    public class DemoDbContext : DbContext
    {
        public DbSet<User>
    }
}
