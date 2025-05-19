namespace Database.Context.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Resolution { get; set; }
        public string Statut { get; set; } = string.Empty;
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset? ClosingDate { get; set; }

        public int ProduitSystemeExploitationVersionId { get; set; }
        public virtual Produit_SystemeExploitation_Version? ProduitSystemeExploitationVersion { get; set; }
    }
}
