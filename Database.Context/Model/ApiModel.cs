using System.Globalization;

namespace Database.Context.Model
{
    public class ApiModel
    {
        public string Product { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string OS { get; set; } = string.Empty;
        public DateTimeOffset CreationDate { get; set; }
        public string Statut { get; set; } = string.Empty;
        public DateTimeOffset? ClosingDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Resolution { get; set; }
    }
}
