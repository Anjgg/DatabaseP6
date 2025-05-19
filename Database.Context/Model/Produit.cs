namespace Database.Context.Model
{
    public class Produit
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual IEnumerable<Produit_SystemeExploitation_Version>? Produit_SystemeExploitation_Versions { get; set; }
    }
}
