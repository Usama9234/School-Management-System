using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using School_Management_System.Data;
using School_Management_System.Models.Admin;

namespace School_Management_System.Controllers.Admin
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext db;

        public StudentController(ApplicationDbContext _db)
        {
            db = _db;
        }


        public IActionResult ViewAllStudents()
        {
            List<StudentDetails> objList = new List<StudentDetails>();
            var stdList = db.students.Include(f => f.Class).ToList();

            // Use a HashSet to track unique classes
            HashSet<int> uniqueClassIds = new HashSet<int>();

            // Filter out duplicate classes while creating objList
            foreach (var student in stdList)
            {
                // Check if the classId is already added to the HashSet
                if (!uniqueClassIds.Contains(student.ClassId))
                {
                    objList.Add(student);
                    uniqueClassIds.Add(student.ClassId);
                }
            }

            return View(objList);
        }

        public IActionResult ViewStudents(int classId)
        {
            List<StudentDetails> objList = new List<StudentDetails>();

            // Fetch only students belonging to the selected class
            var stdList = db.students.Include(f => f.Class)
                                     .Where(s => s.ClassId == classId)
                                     .ToList();

            return View(stdList);

        }

        //Create or Add Students
        public IActionResult AddStudent()
        {
            var classList = db.Class.ToList();
            var model = new StudentDetails
            {
                ClassesList = classList,

            };
            return View(model);
        }

        //Create or Add Student
        [HttpPost]
        public IActionResult AddStudent(StudentDetails obj)
        {
            ModelState.Remove("ClassId");
            ModelState.Remove("Class");
            ModelState.Remove("ClassesList");
            

            if (ModelState.IsValid)
            {
                obj.ClassId = obj.ClassId;
                obj.Class = obj.Class;
                obj.ClassesList = obj.ClassesList;

                if (obj.ClassId<=0)
                {
                    TempData["error"] = "Class Not Selected";
                    var classList1 = db.Class.ToList();
                    var model1 = new StudentDetails { ClassesList = classList1, };
                    model1.ClassesList = db.Class.ToList();
                    return View(model1);
                }

                if (db.students.Any(c => c.StudentName == obj.StudentName || c.Password == obj.Password || c.StudentRollNo == obj.StudentRollNo))
                {
                    ModelState.AddModelError("StudentName", "Student with this name already exists.");
                    ModelState.AddModelError("Password", "Password already exists.");
                    ModelState.AddModelError("StudentRollNo", "Student Roll No already exists.");

                    var classList1 = db.Class.ToList();
                    var model1 = new StudentDetails { ClassesList = classList1, };
                    model1.ClassesList = db.Class.ToList();
                    return View(model1); ;
                }
                db.students.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Student Added Successfully";
                return RedirectToAction("ViewAllStudents");

            }

            var classList = db.Class.ToList();
            var model = new StudentDetails { ClassesList = classList, };

            model.ClassesList = db.Class.ToList();
            return View(model);

        }


        //Edit the Student
        public IActionResult EditStudent(int? id)
        {
            var stdToEdit = db.students.Include(f => f.Class).FirstOrDefault(f => f.StudentId == id);

            if (stdToEdit == null)
            {
                TempData["error"] = "Student not found";
                return RedirectToAction("ViewStudents");
            }

            // Explicitly load the Class property
            db.Entry(stdToEdit).Reference(f => f.Class).Load();

            var classList = db.Class.ToList();

            var model = new StudentDetails
            {
                StudentName = stdToEdit.StudentName,
                ClassId = stdToEdit.ClassId,
                StudentAdress = stdToEdit.StudentAdress,
                ClassesList = classList,
                Class = stdToEdit.Class,
                StudentDOB = stdToEdit.StudentDOB,
                StudentEmail = stdToEdit.StudentEmail,
                StudentId = stdToEdit.StudentId,
                StudentRollNo = stdToEdit.StudentRollNo,
                MobileNumber = stdToEdit.MobileNumber,
                Gender = stdToEdit.Gender,
                Password = stdToEdit.Password,
                
            };

            return View("EditStudent", model);
        }


        [HttpPost]
        public IActionResult EditStudent(StudentDetails obj)
        {
            ModelState.Remove("ClassId");
            ModelState.Remove("Class");
            ModelState.Remove("ClassesList");

            if (ModelState.IsValid)
            {
                obj.ClassId = obj.ClassId;
                obj.Class = obj.Class;
                obj.ClassesList = obj.ClassesList;

                if (obj.ClassId <= 0)
                {
                    TempData["error"] = "Class Not Selected";
                    var classList1 = db.Class.ToList();
                    var model1 = new StudentDetails { ClassesList = classList1, };
                    model1.ClassesList = db.Class.ToList();
                    return View(model1);
                }

                if (db.students.Any(c => c.StudentName == obj.StudentName || c.Password == obj.Password || c.StudentRollNo == obj.StudentRollNo))
                {
                    ModelState.AddModelError("StudentName", "Student with this name already exists.");
                    ModelState.AddModelError("Password", "Password already exists.");
                    ModelState.AddModelError("StudentRollNo", "Student Roll No already exists.");

                    var classList1 = db.Class.ToList();
                    var model1 = new StudentDetails { ClassesList = classList1, };
                    model1.ClassesList = db.Class.ToList();
                    return View(model1); ;
                }
                db.students.Update(obj);
                db.SaveChanges();
                TempData["success"] = "Student Updated Successfully";
                return RedirectToAction("ViewAllStudents");
            }

            var classList = db.Class.ToList();
            var model = new StudentDetails { ClassesList = classList, };
            model.ClassesList = db.Class.ToList();
            return View(model);
        }


        // Delete the Student
        public IActionResult DeleteStudent(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            StudentDetails? categoryFromDb = db.students.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        //Delete the Student
        [HttpPost, ActionName("DeleteStudent")]
        public IActionResult DeleteStd(int? id)
        {
            var subjectToDelete = db.students.Find(id);

            if (subjectToDelete != null)
            {
                db.students.Remove(subjectToDelete);
                db.SaveChanges();
                TempData["success"] = "Student Deleted Successfully";
            }
            else
            {
                TempData["error"] = "Subject not found";
            }

            return RedirectToAction("ViewAllStudents");
        }


        
    }
}
