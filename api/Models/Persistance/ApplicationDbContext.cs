using Microsoft.EntityFrameworkCore;

namespace api.Models.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Ekipman> Ekipman { get; set; }
        public DbSet<Klinik> Klinik { get; set; }
    }
}