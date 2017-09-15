using Microsoft.EntityFrameworkCore;

namespace PANServices.Models
{
    public class PanimalContext : DbContext
    {
        public PanimalContext(DbContextOptions<PanimalContext> options)
            : base(options)
        {
        }

        public DbSet<Panimal> PanimalItems { get; set; }

    }
}