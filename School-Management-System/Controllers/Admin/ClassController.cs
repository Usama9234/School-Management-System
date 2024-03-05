using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.Models.Admin;
using System.Security.Claims;

namespace School_Management_System.Controllers.Admin
{
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext db;

        public ClassController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult ViewClasses()
        {
            List<Classes> objList = db.Class.ToList();
            return View(objList);
        }


        //Create or Add New Class
        public IActionResult CreateClass()
        {
            return View();
        }

        //Create or Add New Class
        [HttpPost]
        public IActionResult CreateClass(Classes obj)
        {

            if (ModelState.IsValid)
            {
                if (db.Class.Any(c => c.ClassName == obj.ClassName))
                {
                    ModelState.AddModelError("ClassName", "Class with this name already exists.");
                    return View();
                }
                db.Class.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Class Created Successfully";
                return RedirectToAction("ViewClasses");
            }
            return View();
        }


        //Edit the Class
        public IActionResult EditClass(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Classes? categoryFromDb = db.Class.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        [HttpPost]
        public IActionResult EditClass(Classes obj)
        {
            if (ModelState.IsValid)
            {
                // Check if another class with the same name already exists
                if (db.Class.Any(c => c.ClassName == obj.ClassName && c.ClassId != obj.ClassId))
                {
                    ModelState.AddModelError("ClassName", "Class with this name already exists.");
                    return View();
                }

                // Retrieve the original class
                var originalClass = db.Class.Find(obj.ClassId);

                if (originalClass != null)
                {
                    // Delete associated subjects
                    var subjectsToDelete = db.subjets.Where(s => s.ClassId == obj.ClassId);
                    db.subjets.RemoveRange(subjectsToDelete);

                    // Delete associated fees
                    var feesToDelete = db.fees.Where(f => f.ClassId == obj.ClassId);
                    db.fees.RemoveRange(feesToDelete);

                    // Update class information
                    originalClass.ClassName = obj.ClassName;

                    db.SaveChanges();
                    TempData["success"] = "Class Updated Successfully";
                }
                else
                {
                    TempData["error"] = "Class not found";
                }

                return RedirectToAction("ViewClasses");
            }

            return View();
        }


        //Delete the Class
        public IActionResult DeleteClass(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Classes? categoryFromDb = db.Class.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //Delete the Class
        [HttpPost, ActionName("DeleteClass")]
        public IActionResult DeletePost(int? id)
        {
            Classes? obj = db.Class.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            db.Class.Remove(obj);
            db.SaveChanges();
            TempData["success"] = "Class Deleted Successfully";
            return RedirectToAction("ViewClasses");

        }


        // View All Subjects
        public IActionResult ViewAllSubjects()
        {
            List<Subjects> objList = new List<Subjects>();
            var stdList = db.subjets.Include(f => f.Class).ToList();

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


        public IActionResult ViewSubjects(int classId)
        {
            List<Subjects> objList = new List<Subjects>();

            // Fetch only students belonging to the selected class
            var stdList = db.subjets.Include(f => f.Class)
                                     .Where(s => s.ClassId == classId)
                                     .ToList();

            return View(stdList);
        }


        //Create or Add New Subjects
        public IActionResult AddSubjects()
        {
            var classList = db.Class.ToList();
            var model = new Subjects
            {
                ClassesList = classList,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddSubjects(Subjects obj)
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
                    var classList2 = db.Class.ToList();
                    var model2 = new Subjects { ClassesList = classList2, };
                    model2.ClassesList = db.Class.ToList();
                    return View(model2);

                }

                // Check if a record with the given Subject already exists
                var existingRecord = db.subjets.FirstOrDefault(f => f.SubjectName == obj.SubjectName && f.ClassId == obj.ClassId);

                if (existingRecord != null)
                {
                    ModelState.AddModelError("SubjectName", "Subject with this name already exists for selected class");
                    var classList1 = db.Class.ToList();
                    var model1 = new Subjects { ClassesList = classList1, };

                    model1.ClassesList = db.Class.ToList();
                    return View(model1);
                }
                else
                {
                    // If it doesn't exist, add a new record
                    db.subjets.Add(obj);
                    db.SaveChanges();
                    TempData["success"] = "Subject Added Successfully";
                }

                return RedirectToAction("ViewAllSubjects");
            }

            var classList = db.Class.ToList();
            var model = new Subjects { ClassesList = classList, };
            model.ClassesList = db.Class.ToList();
            return View(model);
        }

        
        //Edit the Subject
        public IActionResult EditSubject(int? id)
        {
            var subjectToEdit = db.subjets.Include(f => f.Class).FirstOrDefault(f => f.SubjectId == id);

            if (subjectToEdit == null)
            {
                TempData["error"] = "Fee not found";
                return RedirectToAction("ViewFees");
            }

            // Explicitly load the Class property
            db.Entry(subjectToEdit).Reference(f => f.Class).Load();

            var classList = db.Class.ToList();

            var model = new Subjects
            {
                SubjectId = subjectToEdit.SubjectId,
                ClassId = subjectToEdit.ClassId,
                SubjectName = subjectToEdit.SubjectName,
                ClassesList = classList,
                Class = subjectToEdit.Class // Ensure the Class property is set
            };

            return View("EditSubject", model);
        }


        //Edit the Subject
        [HttpPost]
        public IActionResult EditSubject(Subjects obj)
        {

            ModelState.Remove("ClassId");
            ModelState.Remove("Class");
            ModelState.Remove("ClassesList");
            if (ModelState.IsValid)
            {
                obj.ClassId = obj.ClassId;
                obj.Class = obj.Class;
                obj.ClassesList = obj.ClassesList;
                // Check if a record with the given Subject and Class already exists
                var existingRecord = db.subjets.FirstOrDefault(f => f.SubjectName == obj.SubjectName && f.ClassId == obj.ClassId);

                if (existingRecord != null && existingRecord.SubjectId != obj.SubjectId)
                {
                    // If it exists and is not the current subject being edited, return an error
                    ModelState.AddModelError("SubjectName", "Subject with this name already exists for selected class.");
                    var classList1 = db.Class.ToList();
                    var model1 = new Subjects { ClassesList = classList1 };
                    model1.ClassesList = db.Class.ToList();
                    return View(model1);
                }
                else
                {
                    var subject = db.subjets.FirstOrDefault(f => f.ClassId == obj.ClassId);
                    if (subject != null)
                    {
                        // If it exists, update the existing record
                        subject.SubjectName = obj.SubjectName;
                        db.SaveChanges();
                        TempData["success"] = "Subject Updated Successfully";
                    }
                    else
                    {
                        // If it doesn't exist, add a new record
                        db.subjets.Add(obj);
                        db.SaveChanges();
                        TempData["success"] = "Subject Added Successfully";
                    }
                }
                return RedirectToAction("ViewSubjects");

            }

            var classList = db.Class.ToList();
            var model = new Subjects { ClassesList = classList };

            model.ClassesList = db.Class.ToList();
            return View(model);
        }



        // Delete the subject
        public IActionResult DeleteSubject(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Subjects? categoryFromDb = db.subjets.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        //Delete the Class
        [HttpPost, ActionName("DeleteSubject")]
        public IActionResult DeleteSubj(int? id)
        {
            var subjectToDelete = db.subjets.Find(id);

            if (subjectToDelete != null)
            {
                db.subjets.Remove(subjectToDelete);
                db.SaveChanges();
                TempData["success"] = "Subject Deleted Successfully";
            }
            else
            {
                TempData["error"] = "Subject not found";
            }

            return RedirectToAction("ViewSubjects");
        }


        public IActionResult ViewFees()
        {
            List<Fees> objList = new List<Fees>();
            var feesList = db.fees.Include(f => f.Class).ToList();
            return View(feesList);
        }

        //Create or Add New Fees
        public IActionResult AddFees()
        {
            var classList = db.Class.ToList();
            var model = new Fees
            {
                ClassesList = classList,

            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddFees(Fees obj)
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
                    var model1 = new Fees { ClassesList = classList1, };
                    model1.ClassesList = db.Class.ToList();
                    return View(model1);
                }
                // Check if a record with the given ClassId already exists
                var existingRecord = db.fees.FirstOrDefault(f => f.ClassId == obj.ClassId);

                if (existingRecord != null)
                {
                    // If it exists, update the existing record
                    existingRecord.FeeAmount = obj.FeeAmount;
                    db.SaveChanges();
                    TempData["success"] = "Fee Updated Successfully";
                }
                else
                {
                    // If it doesn't exist, add a new record
                    db.fees.Add(obj);
                    db.SaveChanges();
                    TempData["success"] = "Fee Added Successfully";
                }

                return RedirectToAction("ViewFees");
            }



            var classList = db.Class.ToList();
            var model = new Fees { ClassesList = classList, };

            model.ClassesList = db.Class.ToList();
            return View(model);

        }


        //Edit the Fees
        public IActionResult EditFees(int? id)
        {
            var feeToEdit = db.fees.Include(f => f.Class).FirstOrDefault(f => f.FeeId == id);

            if (feeToEdit == null)
            {
                TempData["error"] = "Fee not found";
                return RedirectToAction("ViewFees");
            }

            // Explicitly load the Class property
            db.Entry(feeToEdit).Reference(f => f.Class).Load();

            var classList = db.Class.ToList();

            var model = new Fees
            {
                FeeId = feeToEdit.FeeId,
                ClassId = feeToEdit.ClassId,
                FeeAmount = feeToEdit.FeeAmount,
                ClassesList = classList,
                Class = feeToEdit.Class // Ensure the Class property is set
            };

            return View("EditFees", model);
        }


        //Edit the Fees
        [HttpPost]
        public IActionResult EditFees(Fees obj)
        {
            if (obj.FeeAmount <= 0 || obj.ClassId <= 0)
            {
                TempData["error"] = "Invalid Fee Amount or Class not selected";
                var classList = db.Class.ToList();
                var model = new Fees
                {
                    ClassesList = classList,

                };
                model.ClassesList = db.Class.ToList();
                return View(model);
            }

            // Check if a record with the given ClassId already exists
            var existingRecord = db.fees.FirstOrDefault(f => f.ClassId == obj.ClassId);

            if (existingRecord != null)
            {
                // If it exists, update the existing record
                existingRecord.FeeAmount = obj.FeeAmount;
                db.SaveChanges();
                TempData["success"] = "Fee Updated Successfully";
            }
            else
            {
                // If it doesn't exist, add a new record
                db.fees.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Fee Added Successfully";
            }

            return RedirectToAction("ViewFees");
        }
    }
}
