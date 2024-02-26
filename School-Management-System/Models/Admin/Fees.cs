using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models.Admin
{
    public class Fees
    {
        [Key]
        public int FeeId { get; set; }

        [Required]
        public int ClassId { get; set; }

        public virtual Classes Class { get; set; }

        [Required]
        public List<Classes> ClassesList { get; set; }

        [Required]
        public int FeeAmount { get; set; }
    }
}
