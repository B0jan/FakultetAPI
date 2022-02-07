using System.ComponentModel.DataAnnotations;

namespace Fakultet.Dtos
{
    public class PredmetCreateDto
    {
        [Required]
        public string ImePredmeta { get; set; }
        [Required]
        public int BrCasova { get; set; }
        [Required]
        public string Predavac { get; set; }
    }
}