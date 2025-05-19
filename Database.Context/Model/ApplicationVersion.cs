namespace Database.Context.Model
{
    public class ApplicationVersion
    {
        public int Id { get; set; }

        public string NumVersion { get; set; } = string.Empty;

        public virtual IEnumerable<Produit_SystemeExploitation_Version>? Produit_SystemeExploitation_Versions { get; set; }

    }
}
