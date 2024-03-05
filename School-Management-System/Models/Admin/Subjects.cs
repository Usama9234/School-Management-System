using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models.Admin
{
    public class Subjects
    {
        [Key]
        public int SubjectId { get; set; }

        [Required]
        public int ClassId { get; set; }

        public virtual Classes Class { get; set; }

        [Required]
        public List<Classes> ClassesList { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string SubjectName { get; set; }
    }
}
