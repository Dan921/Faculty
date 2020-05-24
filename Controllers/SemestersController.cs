using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Controllers
{
    public class SemestersController : Controller
    {
        FacultyContext db = new FacultyContext();

        // GET: Semesters
        public ActionResult Index()
        {
            return View(db.Semesters.ToList());
        }

        // GET: Semesters/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Semesters.FirstOrDefault(p => p.SemesterId == id));
        }

        // GET: Semesters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Semesters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Semester semester)
        {
            try
            {
                db.Semesters.Add(semester);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Semesters/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.SemesterId = id;
            return View(db.Semesters.FirstOrDefault(p => p.SemesterId == id));
        }

        // POST: Semesters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Semester semester)
        {
            try
            {
                Semester seme = db.Semesters.Find(id);
                seme.SemesterNumber = semester.SemesterNumber;
                db.Entry(seme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Semesters/Delete/5
        public ActionResult Delete(int id)
        {
            return View(db.Semesters.FirstOrDefault(p => p.SemesterId == id));
        }

        // POST: Semesters/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Semester seme = db.Semesters.FirstOrDefault(p => p.SemesterId == id);
                db.Remove(seme);
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

        public ActionResult SubjectsForSemesterId(int id)
        {
            ViewBag.Subjects = db.Subjects;
            ViewBag.SemesterNumber = db.Semesters.FirstOrDefault(p => p.SemesterId == id).SemesterNumber;
            ViewBag.SemesterId = id;
            return View(db.Semesters_subjects.Where(p => p.SemesterId == id).ToList());
        }

        public ActionResult DeleteSubjectsForSemesterId(int subjectId, int semesterId)
        {
            SubjectsForSemester subjectsForSemester = db.Semesters_subjects.FirstOrDefault(p => p.SubjectId == subjectId && p.SemesterId == semesterId);
            db.Semesters_subjects.Remove(subjectsForSemester);
            db.SaveChanges();
            return RedirectToAction("SubjectsForSemesterId", new { id = semesterId });
        }

        public ActionResult CreateSubjectsForSemesterId(int id)
        {
            ViewBag.SemesterId = id;
            ViewBag.SemesterNumber = db.Semesters.FirstOrDefault(p => p.SemesterId == id).SemesterNumber;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubjectsForSemesterId(SubjectsForSemester subjectsForSemester)
        {
            try
            {
                db.Semesters_subjects.Add(subjectsForSemester);
                db.SaveChanges();
                return RedirectToAction("SubjectsForSemesterId", new { id = subjectsForSemester.SemesterId });
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
    }
}