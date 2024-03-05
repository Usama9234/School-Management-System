using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.Models.Admin;
using Microsoft.AspNetCore.Http;


namespace School_Management_System.Controllers.Admin
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext db;

        public TeacherController(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IActionResult ViewTeachers()
        {
            List<TeacherDetails> objList = db.Teachers.ToList();
            return View(objList);
        }

        public IActionResult ViewAllTeachers()
        {
            List<TeacherDetails> objList = new List<TeacherDetails>();
            /*var stdList = db.teachers.Include(f => f.Class).ToList();*/

            // Use a HashSet to track unique classes
            HashSet<int> uniqueClassIds = new HashSet<int>();

            // Filter out duplicate classes while creating objList
            /*foreach (var student in stdList)
            {
                // Check if the classId is already added to the HashSet
                if (!uniqueClassIds.Contains(student.ClassId))
                {
                    objList.Add(student);
                    uniqueClassIds.Add(student.ClassId);
                }
            }*/

            return View();
        }

        //Create or Add Teacher
        public IActionResult AddTeacher()
        {
            return View();
        }

        //Create or Add Teacher
        [HttpPost]
        public IActionResult AddTeacher(TeacherDetails obj)
        {
            ModelState.Remove("SubjectId");
            ModelState.Remove("Subject");
            ModelState.Remove("SubjectsList");
            ModelState.Remove("TeachersList");

            if (ModelState.IsValid)
            {
                if (db.Teachers.Any(c => c.TeacherName == obj.TeacherName))
                {
                    ModelState.AddModelError("TeacherName", "Teacher with this name already exists.");
                    return View();
                }
                if (db.Teachers.Any(c => c.Password == obj.Password))
                {
                    ModelState.AddModelError("Password", "Password already exists.");
                    return View();
                }
                if (db.Teachers.Any(c => c.MobileNumber == obj.MobileNumber))
                {
                    ModelState.AddModelError("MobileNumber", "Mobile Number already exists.");
                    return View();
                }
                if (db.Teachers.Any(c => c.TeacherEmail == obj.TeacherEmail))
                {
                    ModelState.AddModelError("TeacherEmail", "Email already exists.");
                    return View();
                }
                db.Teachers.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Teacher Added Successfully";
                return RedirectToAction("ViewTeachers");
            }
            return View();

        }


        //Edit the Teacher
        public IActionResult EditTeacher(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            TeacherDetails? categoryFromDb = db.Teachers.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        [HttpPost]
        public IActionResult EditTeacher(TeacherDetails obj)
        {
            if (ModelState.IsValid)
            {
                if (db.Teachers.Any(c => c.TeacherName == obj.TeacherName ))
                {
                    ModelState.AddModelError("TeacherName", "Teacher with this name already exists.");
                    return View();
                }
                if (db.Teachers.Any(c => c.Password == obj.Password))
                {
                    ModelState.AddModelError("Password", "Password already exists.");
                    return View();
                }
                if (db.Teachers.Any(c => c.MobileNumber == obj.MobileNumber))
                {
                    ModelState.AddModelError("MobileNumber", "Mobile Number already exists.");
                    return View();
                }
                if (db.Teachers.Any(c => c.TeacherEmail == obj.TeacherEmail))
                {
                    ModelState.AddModelError("TeacherEmail", "Email already exists.");
                    return View();
                }
                db.Teachers.Update(obj);
                db.SaveChanges();
                TempData["success"] = "Teacher Updated Successfully";
                return RedirectToAction("ViewTeachers");
            }

            return View();
        }


        // Delete the Teacher
        public IActionResult DeleteTeacher(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            TeacherDetails? categoryFromDb = db.Teachers.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        //Delete the Class
        [HttpPost, ActionName("DeleteTeacher")]
        public IActionResult DeleteStaff(int? id)
        {
            var teacherToDelete = db.Teachers.Find(id);

            if (teacherToDelete != null)
            {
                db.Teachers.Remove(teacherToDelete);
                db.SaveChanges();
                TempData["success"] = "Teacher Deleted Successfully";
            }
            else
            {
                TempData["error"] = "Teacher not found";
            }

            return RedirectToAction("ViewTeachers");

        }



        public IActionResult GetSubjects(int classId)
        {
            var subjects = db.subjets
                .Where(s => s.ClassId == classId)
                .Select(s => new { s.SubjectId, s.SubjectName })
                .ToList();

            return Json(subjects);
        }



        public IActionResult AssignSubject()
        {
            var classList = db.Class.ToList();
            var teacherList = db.Teachers.ToList();
            var subjectsList = db.subjets.ToList();

            // Assuming you want to initialize the properties of AssignedSubjects
            var model = new AssignedSubjects
            {
                SubjectList = subjectsList,      // Assuming Subjects is the DbSet for subjects in your DbContext
                TeachersList = teacherList,
                ClassesList = classList
                // You might need to assign values to other properties like SubjectId, TeacherId, and ClassId based on your logic
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult AssignSubject(AssignedSubjects obj)
        {
            var classList = db.Class.ToList();
            var teacherList = db.Teachers.ToList();
            var subjectsList = db.subjets.ToList();

            var model = new AssignedSubjects
            {
                SubjectList = subjectsList,
                TeachersList = teacherList,
                ClassesList = classList
                // You might need to assign values to other properties like SubjectId, TeacherId, and ClassId based on your logic
            };

            if (obj.ClassId <= 0 || obj.TeacherId <= 0)
            {
                TempData["error"] = "Please select Teacher, Class";
                ModelState.Remove("ClassId");
                return View(model);
            }

            if (obj.SubjectId <= 0)
            {
                TempData["error"] = "Please select Subject";
                ModelState.Remove("ClassId");
                obj.ClassId = 0;
                return View(model);
            }

            var selectedTeacherName = teacherList.FirstOrDefault(t => t.TeacherId == obj.TeacherId)?.TeacherName;
            var selectedSubjectName = subjectsList.FirstOrDefault(s => s.SubjectId == obj.SubjectId)?.SubjectName;
            var selectedClassName = classList.FirstOrDefault(c => c.ClassId == obj.ClassId)?.ClassName;

            // Find the existing AssignedSubjects record for the given Subject and Class
            var existingTeacherDetails = db.assignedSubjects
                .FirstOrDefault(t => t.SubjectId == obj.SubjectId && t.ClassId == obj.ClassId);

            if (existingTeacherDetails != null)
            {
                // If a record exists, update it with the new TeacherId
                existingTeacherDetails.TeacherId = obj.TeacherId;
                existingTeacherDetails.Teachername= selectedTeacherName;
            }
            else
            {
                // If a record doesn't exist, create a new AssignedSubjects record
                var newTeacherDetails = new AssignedSubjects
                {
                    TeacherId = obj.TeacherId,
                    ClassId = obj.ClassId,
                    SubjectId = obj.SubjectId,
                    // Populate other properties of AssignedSubjects as needed
                };

                // Assign the names to the corresponding properties
                newTeacherDetails.Teachername = selectedTeacherName;
                newTeacherDetails.SubjectName = selectedSubjectName;
                newTeacherDetails.ClassName = selectedClassName;

                // Add the new record to the database
                db.assignedSubjects.Add(newTeacherDetails);
            }

            db.SaveChanges();
            TempData["success"] = "Subject Assigned Successfully";
            return RedirectToAction("ViewAssignSubjects");
        }


        public IActionResult ViewAssignSubjects()
        {
            List<AssignedSubjects> objList = db.assignedSubjects.ToList();
            return View(objList);
        }


        public IActionResult EditAssignSubject(int id)
        {
            // Fetch the assigned subject based on the id
            var assignedSubject = db.assignedSubjects.FirstOrDefault(t => t.AssignedSubjectId == id);
            if (assignedSubject.Teachername != null)
            {
                HttpContext.Session.SetString("TeacherName", assignedSubject.Teachername);
                HttpContext.Session.SetInt32("TeacherId", assignedSubject.TeacherId);

            }

            if (assignedSubject == null)
            {
                return NotFound();
            }

            // Populate lists for dropdowns (similar to your GET action)
            var classList = db.Class.ToList();
            var teacherList = db.Teachers.ToList();
            var subjectsList = db.subjets.ToList();

            var model = new AssignedSubjects
            {
                AssignedSubjectId = assignedSubject.AssignedSubjectId,
                TeacherId = assignedSubject.TeacherId,
                SubjectId = assignedSubject.SubjectId,
                SubjectList = subjectsList,
                TeachersList = teacherList,
                ClassesList = classList,
                Teachername = assignedSubject.Teachername,
                ClassName = assignedSubject.ClassName,
                SubjectName = assignedSubject.SubjectName

            };
            return View(model);
        }


        [HttpPost]
        public IActionResult EditAssignSubject(AssignedSubjects obj)
        {
            var classList = db.Class.ToList();
            var teacherList = db.Teachers.ToList();
            var subjectsList = db.subjets.ToList();
            obj.TeacherId= HttpContext.Session.GetInt32("TeacherId")??0;
            var model = new AssignedSubjects
            {
                SubjectList = subjectsList,
                TeachersList = teacherList,
                ClassesList = classList
                // You might need to assign values to other properties like SubjectId, TeacherId, and ClassId based on your logic
            };

            if (obj.ClassId <= 0 || obj.SubjectId <= 0)
            {
                TempData["error"] = "Please select both Class and Subject";
                ModelState.Remove("ClassId");
                return View(model);
            }

            var selectedTeacherName = teacherList.FirstOrDefault(t => t.TeacherId == obj.TeacherId)?.TeacherName;
            var selectedSubjectName = subjectsList.FirstOrDefault(s => s.SubjectId == obj.SubjectId)?.SubjectName;
            var selectedClassName = classList.FirstOrDefault(c => c.ClassId == obj.ClassId)?.ClassName;

            // Find the existing AssignedSubjects record for the given Subject and Class
            var existingTeacherDetails = db.assignedSubjects
                .FirstOrDefault(t => t.SubjectId == obj.SubjectId && t.ClassId == obj.ClassId);

            if (existingTeacherDetails != null)
            {
                // If a record exists, update it with the new TeacherId
                existingTeacherDetails.TeacherId = obj.TeacherId;
                existingTeacherDetails.Teachername = selectedTeacherName;
            }
            else
            {
                // If a record doesn't exist, create a new AssignedSubjects record
                var newTeacherDetails = new AssignedSubjects
                {
                    TeacherId = obj.TeacherId,
                    ClassId = obj.ClassId,
                    SubjectId = obj.SubjectId,
                    // Populate other properties of AssignedSubjects as needed
                };

                // Assign the names to the corresponding properties
                newTeacherDetails.Teachername = selectedTeacherName;
                newTeacherDetails.SubjectName = selectedSubjectName;
                newTeacherDetails.ClassName = selectedClassName;

                // Add the new record to the database
                db.assignedSubjects.Add(newTeacherDetails);
            }

            db.SaveChanges();
            TempData["success"] = "Subject Assigned Successfully";
            return RedirectToAction("ViewAssignSubjects");

        }


        public IActionResult DeleteAssignSubject(int id)
        {
            // Fetch the assigned subject based on the id
            var assignedSubject = db.assignedSubjects.FirstOrDefault(t => t.AssignedSubjectId == id);
            if (assignedSubject.Teachername != null)
            {
                HttpContext.Session.SetString("TeacherName", assignedSubject.Teachername);
                HttpContext.Session.SetInt32("TeacherId", assignedSubject.TeacherId);

            }

            if (assignedSubject == null)
            {
                return NotFound();
            }

            // Populate lists for dropdowns (similar to your GET action)
            var classList = db.Class.ToList();
            var teacherList = db.Teachers.ToList();
            var subjectsList = db.subjets.ToList();

            var model = new AssignedSubjects
            {
                AssignedSubjectId = assignedSubject.AssignedSubjectId,
                TeacherId = assignedSubject.TeacherId,
                SubjectId = assignedSubject.SubjectId,
                SubjectList = subjectsList,
                TeachersList = teacherList,
                ClassesList = classList,
                Teachername = assignedSubject.Teachername,
                ClassName = assignedSubject.ClassName,
                SubjectName = assignedSubject.SubjectName

            };
            return View(model);
        }


        [HttpPost, ActionName("DeleteAssignSubject")]
        public IActionResult DeleteAssignedSubject(int? id)
        {
            var subToDelete = db.assignedSubjects.Find(id);

            if (subToDelete != null)
            {
                db.assignedSubjects.Remove(subToDelete);
                db.SaveChanges();
                TempData["success"] = "Deleted Successfully";
            }
            else
            {
                TempData["error"] = "Teacher not found";
            }

            return RedirectToAction("ViewAssignSubjects");

        }


        public IActionResult ViewSalaries()
        {
            List<StaffSalary> objList = new List<StaffSalary>();
            var salaryList = db.staffSalaries.Include(f => f.Teacher).ToList();
            return View(salaryList);
        }

        //Create or Add New Salary
        public IActionResult AddSalary()
        {
            var teacherList = db.Teachers.ToList();
            var model = new StaffSalary
            {
                TeacherLists = teacherList,

            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddSalary(StaffSalary obj)
        {
            ModelState.Remove("TeacherId");
            ModelState.Remove("Teacher");
            ModelState.Remove("TeacherLists");


            if (ModelState.IsValid)
            {
                obj.TeacherId = obj.TeacherId;
                obj.Teacher = obj.Teacher;
                obj.TeacherLists = obj.TeacherLists;

                if (obj.TeacherId <= 0)
                {
                    TempData["error"] = "Teacher Not Selected";
                    var teacherList1 = db.Teachers.ToList();
                    var model1 = new StaffSalary { TeacherLists = teacherList1, };
                    model1.TeacherLists = db.Teachers.ToList();
                    return View(model1);
                }
                // Check if a record with the given Teacher already exists
                var existingRecord = db.staffSalaries.FirstOrDefault(f => f.TeacherId == obj.TeacherId);

                if (existingRecord != null)
                {
                    // If it exists, update the existing record
                    existingRecord.SalaryAmount = obj.SalaryAmount;
                    db.SaveChanges();
                    TempData["success"] = "Salary Updated Successfully";
                }
                else
                {
                    // If it doesn't exist, add a new record
                    db.staffSalaries.Add(obj);
                    db.SaveChanges();
                    TempData["success"] = "Salary Added Successfully";
                }

                return RedirectToAction("ViewSalaries");
            }

            var teacherList = db.Teachers.ToList();
            var model = new StaffSalary { TeacherLists = teacherList, };

            model.TeacherLists = db.Teachers.ToList();
            return View(model);

        }


        //Edit the Fees
        public IActionResult EditSalary(int? id)
        {
            var salaryToEdit = db.staffSalaries.Include(f => f.Teacher).FirstOrDefault(f => f.Id == id);

            if (salaryToEdit == null)
            {
                TempData["error"] = "Salary not found";
                return RedirectToAction("ViewSalaries");
            }

            // Explicitly load the Teacher property
            db.Entry(salaryToEdit).Reference(f => f.Teacher).Load();

            var teacherList = db.Teachers.ToList();

            var model = new StaffSalary
            {
                Id = salaryToEdit.Id,
                TeacherId = salaryToEdit.TeacherId,
                SalaryAmount = salaryToEdit.SalaryAmount,
                TeacherLists = teacherList,
                Teacher = salaryToEdit.Teacher // Ensure the Class property is set
            };

            return View("EditSalary", model);
        }


        //Edit the Salary
        [HttpPost]
        public IActionResult EditSalary(StaffSalary obj)
        {
            if (obj.SalaryAmount <= 0 || obj.TeacherId <= 0)
            {
                TempData["error"] = "Invalid Salary Amount or Teacher not selected";
                var teacherList = db.Teachers.ToList();
                var model = new StaffSalary
                {
                    TeacherLists = teacherList,

                };
                model.TeacherLists = db.Teachers.ToList();
                return View(model);
            }

            // Check if a record with the given ClassId already exists
            var existingRecord = db.staffSalaries.FirstOrDefault(f => f.TeacherId == obj.TeacherId);

            if (existingRecord != null)
            {
                // If it exists, update the existing record
                existingRecord.SalaryAmount = obj.SalaryAmount;
                db.SaveChanges();
                TempData["success"] = "Salary Updated Successfully";
            }
            else
            {
                // If it doesn't exist, add a new record
                db.staffSalaries.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Salary Added Successfully";
            }

            return RedirectToAction("ViewSalaries");
        }

    }
}
