using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakultet.Models
{
    public class Predmet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImePredmeta { get; set; }

        [Required]
        public int BrCasova { get; set; }

        [Required]
        public string Predavac { get; set; }
    }
}