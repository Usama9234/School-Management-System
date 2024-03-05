using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models.Admin
{
    public class StaffSalary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }

        public virtual TeacherDetails Teacher { get; set; }

        [Required]
        public List<TeacherDetails> TeacherLists { get; set; }

        [Required(ErrorMessage = "Fee amount is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid fee amount ")]
        public int SalaryAmount { get; set; }
    }
}
