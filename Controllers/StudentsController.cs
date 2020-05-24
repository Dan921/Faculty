using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Controllers
{
    public class StudentsController : Controller
    {
        FacultyContext db = new FacultyContext();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList().OrderBy(s=>s.SecondName).ThenBy(s=>s.FirstName));
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Students.FirstOrDefault(p => p.StudentId == id));
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Students.FirstOrDefault(p => p.StudentId == id));
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                Student stud = db.Students.FirstOrDefault(p => p.StudentId == id);
                db.Students.Remove(stud);
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            return View(db.Students.FirstOrDefault(p => p.StudentId == id));
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Student student)
        {
            try
            {
                Student stud = db.Students.FirstOrDefault(p => p.StudentId == id);
                db.Students.Remove(stud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult AcademicPlanForStudentId(int id)
        {
            ViewBag.Subjects = db.Subjects;
            ViewBag.Semesters = db.Semesters;
            ViewBag.FirstName = db.Students.FirstOrDefault(p => p.StudentId == id).FirstName;
            ViewBag.SecondName = db.Students.FirstOrDefault(p => p.StudentId == id).SecondName;
            return View(db.AcademicPlan.Where(p => p.StudentId == id).ToList());
        }
    }
}