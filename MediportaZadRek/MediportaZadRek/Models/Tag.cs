using System.ComponentModel.DataAnnotations;

namespace MediportaZadRek.Models
{
    public class Tag
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^\d+$")]
        [Range(0, 9999999999999999.99)]
        public decimal Count { get; set; }

        [RegularExpression(@"^\d+([\.,]\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        public decimal PercentagePopulation { get; set; }
    }
}
