using Database.Context.Model;
using Microsoft.EntityFrameworkCore;

namespace Database.Context.Service
{
    public class ApplicationService : IApplicationService
    {
        public readonly ApplicationDbContext _context;

        public ApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetProduitsAsync()
        {
            var a = await _context.Tickets
                                  .Include(t => t.ProduitSystemeExploitationVersion)
                                    .ThenInclude(p => p.SystemeExploitation)
                                  .Include(p => p.ProduitSystemeExploitationVersion)
                                    .ThenInclude(p => p.Version)
                                  .Include(p => p.ProduitSystemeExploitationVersion)
                                    .ThenInclude(p => p.Produit)
                                  .ToListAsync();
            return a;
        }

        public async Task<int> AddAsync(List<ApiModel> requete)
        {
            if (requete == null || requete.Count == 0)
                throw new ArgumentNullException(nameof(requete), "Request cannot be null or empty");
            var nbOps = 0;

            foreach (var item in requete)
            {
                // Vérifie ou crée les entités de base
                var existingProduct = await _context.Produits.FirstOrDefaultAsync(p => p.Name == item.Product)
                    ?? new Produit { Name = item.Product };

                var existingVersion = await _context.Versions.FirstOrDefaultAsync(v => v.NumVersion == item.Version)
                    ?? new ApplicationVersion { NumVersion = item.Version };

                var existingOS = await _context.SystemesExploitation.FirstOrDefaultAsync(se => se.Name == item.OS)
                    ?? new SystemeExploitation { Name = item.OS };

                // Ajoute à la DB s’ils sont nouveaux (pas encore trackés)
                if (existingProduct.Id == 0) _context.Produits.Add(existingProduct);
                if (existingVersion.Id == 0) _context.Versions.Add(existingVersion);
                if (existingOS.Id == 0) _context.SystemesExploitation.Add(existingOS);
                await _context.SaveChangesAsync(); // Nécessaire pour obtenir les IDs

                existingProduct = await _context.Produits.FirstOrDefaultAsync(p => p.Name == item.Product);
                existingVersion = await _context.Versions.FirstOrDefaultAsync(v => v.NumVersion == item.Version);
                existingOS = await _context.SystemesExploitation.FirstOrDefaultAsync(se => se.Name == item.OS);

                var existingCombo = await _context.Produit_SystemeExploitation_Versions
                    .FirstOrDefaultAsync(p => p.ProduitId == existingProduct.Id && p.SystemeExploitationId == existingOS.Id && p.VersionId == existingVersion.Id);

                var ticket = new Ticket()
                {
                    Statut = item.Statut,
                    Description = item.Description,
                    Resolution = item.Resolution,
                    CreationDate = item.CreationDate,
                    ClosingDate = item.ClosingDate,
                    ProduitSystemeExploitationVersion = existingCombo != null ? existingCombo : new Produit_SystemeExploitation_Version()
                    {
                        Produit = existingProduct,
                        Version = existingVersion,
                        SystemeExploitation = existingOS
                    }
                };
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
                nbOps++;
            }
            
            return nbOps;
        }


        public async Task<int> DeleteAllAsync()
        {
            var tickets = await _context.Tickets.ToListAsync();
            _context.Tickets.RemoveRange(tickets);
            var produits = await _context.Produits.ToListAsync();
            _context.Produits.RemoveRange(produits);
            var versions = await _context.Versions.ToListAsync();
            _context.Versions.RemoveRange(versions);
            var systemesExploitation = await _context.SystemesExploitation.ToListAsync();
            _context.SystemesExploitation.RemoveRange(systemesExploitation);
            var produitSystemeExploitationVersions = await _context.Produit_SystemeExploitation_Versions.ToListAsync();
            _context.Produit_SystemeExploitation_Versions.RemoveRange(produitSystemeExploitationVersions);
            var nbOps = await _context.SaveChangesAsync();
            return nbOps;
        }
    }
}
