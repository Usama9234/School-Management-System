using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace School_Management_System.Models.Admin
{
    public class StudentDetails
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string StudentName { get; set; }

        [Required]
        public DateOnly StudentDOB { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Gender { get; set; }

        [Required]
        [StringLength(11)] // Adjust the length based on your requirements
        [RegularExpression(@"^\d+$", ErrorMessage = "Mobile number must contain only numeric characters.")]
        public string MobileNumber { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Email length must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string StudentEmail { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string StudentAdress { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string StudentRollNo { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [BindProperty]
        public int ClassId { get; set; }

        [BindProperty]
        public virtual Classes Class { get; set; }

        [BindProperty]
        public List<Classes> ClassesList { get; set; }
    }
}
