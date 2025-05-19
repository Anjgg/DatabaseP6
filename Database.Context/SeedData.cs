using Database.Context.Model;
using Microsoft.EntityFrameworkCore;

namespace Database.Context
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Tickets.Any())
                {
                    return;   // DB has been seeded
                }

                context.Produit_SystemeExploitation_Versions.AddRange(
                    new Produit_SystemeExploitation_Version
                    {
                        ProduitId = 1,
                        SystemeExploitationId = 1,
                        VersionId = 1
                    },
                    new Produit_SystemeExploitation_Version
                    {
                        ProduitId = 2,
                        SystemeExploitationId = 2,
                        VersionId = 2
                    }
                );
            }
        }
    }
}