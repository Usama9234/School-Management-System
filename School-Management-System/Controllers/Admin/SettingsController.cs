using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.Models.Admin;
using System.Security.Claims;

namespace School_Management_System.Controllers.Admin
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext db;

        public SettingsController(ApplicationDbContext _db)
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


        //Edit the Class
        [HttpPost]
        public IActionResult EditClass(Classes obj)
        {
            if (ModelState.IsValid)
            {
                if (db.Class.Any(c => c.ClassName == obj.ClassName))
                {
                    ModelState.AddModelError("ClassName", "Class with this name already exists.");
                    return View();
                }
                db.Class.Update(obj);
                db.SaveChanges();
                TempData["success"] = "Class Updated Successfully";
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
            if (obj.FeeAmount <= 0 || obj.ClassId <= 0)
            {
                TempData["error"] = "Invalid Fee Amount or Class not selected";
                var classList = db.Class.ToList();
                var model = new Fees { ClassesList = classList, };

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


        public IActionResult ViewSubjects()
        {
            List<Subjets> objList = new List<Subjets>();
            var subjectList = db.subjets.Include(f => f.Class).ToList();
            return View(subjectList);
        }


        //Create or Add New Subjects
        public IActionResult AddSubjects()
        {
            var classList = db.Class.ToList();
            var model = new Subjets
            {
                ClassesList = classList,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddSubjects(Subjets obj)
        {
            if (obj.SubjectName==null || obj.ClassId<=0 )
            {
                TempData["error"] = "Invalid Subject Name or Class not selected";
                var classList = db.Class.ToList();
                var model = new Subjets { ClassesList = classList, };

                model.ClassesList = db.Class.ToList();
                return View(model);
            }

            // Check if a record with the given Subject already exists
            var existingRecord = db.subjets.FirstOrDefault(f => f.SubjectName  == obj.SubjectName && f.ClassId == obj.ClassId);

            if (existingRecord != null)
            {
                TempData["error"] = "Subject with this name already exists";
                var classList = db.Class.ToList();
                var model = new Subjets { ClassesList = classList, };

                model.ClassesList = db.Class.ToList();
                return View(model);
            }
            else
            {
                // If it doesn't exist, add a new record
                db.subjets.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Subjcet Added Successfully";
            }

            return RedirectToAction("ViewSubjects");
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

            var model = new Subjets
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
        public IActionResult EditSubject(Subjets obj)
        {
            if (obj.SubjectName == null || obj.ClassId <= 0)
            {
                TempData["error"] = "Invalid Subject Name or Class not selected";
                var classList = db.Class.ToList();
                var model = new Subjets { ClassesList = classList };

                model.ClassesList = db.Class.ToList();
                return View(model);
            }

            // Check if a record with the given Subject and Class already exists
            var existingRecord = db.subjets.FirstOrDefault(f => f.SubjectName == obj.SubjectName && f.ClassId == obj.ClassId);

            if (existingRecord != null && existingRecord.SubjectId != obj.SubjectId)
            {
                // If it exists and is not the current subject being edited, return an error
                TempData["error"] = "Subject with this name already exists for the selected class";
                var classList = db.Class.ToList();
                var model = new Subjets { ClassesList = classList };

                model.ClassesList = db.Class.ToList();
                return View(model);
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



        // Delete the subject
        public IActionResult DeleteSubject(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Subjets? categoryFromDb = db.subjets.Find(id);
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





    }
}
