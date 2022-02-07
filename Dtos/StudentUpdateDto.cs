using System.ComponentModel.DataAnnotations;

namespace Fakultet.Dtos
{
    public class StudentUpdateDto
    {
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public int Godine { get; set; }
    }
}