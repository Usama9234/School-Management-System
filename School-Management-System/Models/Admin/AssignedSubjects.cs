using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models.Admin
{
    public class AssignedSubjects
    {
        [Key]
        public int AssignedSubjectId { get; set; }

        
        public int SubjectId { get; set; }

        public int TeacherId { get; set; }


        public int ClassId { get; set; }


        public virtual Subjects Subject { get; set; }


        public List<Subjects> SubjectList { get; set; }

        public List<TeacherDetails> TeachersList { get; set; }


        public List<Classes> ClassesList { get; set; }

        public string SubjectName { get; set; }

        public string Teachername { get; set; }

        public string ClassName { get; set; }



    }
}
