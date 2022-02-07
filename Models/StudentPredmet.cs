using System.ComponentModel.DataAnnotations;

namespace Fakultet.Models
{
    public class StudentPredmet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IndexStudenta { get; set; }
        [Required]
        public int IdPredmeta { get; set; }

        public StudentPredmet(int indexStudenta, int idPredmeta)
        {
            IndexStudenta = indexStudenta;
            IdPredmeta = idPredmeta;
        }
    }
}