using System.ComponentModel.DataAnnotations;

namespace Gift_of_Givers.Models
{
    public class AreaOfNeed
    {
        public int AreaOfNeedId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Region { get; set; } = string.Empty;

        // Simple percentage used to position the marker on the map (0-100)
        public int XPercent { get; set; }
        public int YPercent { get; set; }

        [Required]
        public string Severity { get; set; } = "Medium"; // Low | Medium | High | Critical

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;
    }
}
