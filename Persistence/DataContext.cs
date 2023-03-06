using Domain;
using Microsoft.EntityFrameworkCore;


namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // This represent the 'Activity' Table in the DB
        public DbSet<Activity> Activities { get; set; }
    }
}
