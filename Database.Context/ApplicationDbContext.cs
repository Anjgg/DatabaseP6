using Database.Context.Model;
using Microsoft.EntityFrameworkCore;

namespace Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Produit> Produits { get; set; } = null!;
        public virtual DbSet<ApplicationVersion> Versions { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<SystemeExploitation> SystemesExploitation { get; set; } = null!;
        public virtual DbSet<Produit_SystemeExploitation_Version> Produit_SystemeExploitation_Versions { get; set; } = null!;

        public ApplicationDbContext() : base()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
    }
}
