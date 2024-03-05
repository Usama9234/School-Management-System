using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models.Admin
{
    public class TeacherDetails
    {
        [Key]
        public int TeacherId { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string TeacherName { get; set; }

        [Required]
        public DateOnly TeacherDOB { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Gender { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)] // Adjust the length based on your requirements
        [RegularExpression(@"^\d+$", ErrorMessage = "Mobile number must contain only numeric characters.")]
        public string MobileNumber { get; set; }


        [Required]
        [EmailAddress]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email length must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string TeacherEmail { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string TeacherAdress { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<TeacherDetails> TeachersList { get; set; }

    }

}
