using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models.Admin
{
    public class Classes
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ClassName { get; set; }
    }
}
