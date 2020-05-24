using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Controllers
{
    public class SubjectsController : Controller
    {
        FacultyContext db = new FacultyContext();

        // GET: Subjects
        public ActionResult Index()
        {
            return View(db.Subjects.ToList());
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Subjects.FirstOrDefault(p => p.SubjectId == id));
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            try
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Subjects.FirstOrDefault(p => p.SubjectId == id));
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Subject subject)
        {
            try
            {
                Subject subj = db.Subjects.FirstOrDefault(p => p.SubjectId == id);
                db.Subjects.Remove(subj);
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int id)
        {
            return View(db.Subjects.FirstOrDefault(p => p.SubjectId == id));
        }

        // POST: Subjects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Subject subj = db.Subjects.FirstOrDefault(p => p.SubjectId == id);
                db.Subjects.Remove(subj);
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

        public ActionResult AcademicPlanForSubjectId(int id)
        {
            ViewBag.Students = db.Students;
            ViewBag.Semesters = db.Semesters;
            ViewBag.SubjectName= db.Subjects.FirstOrDefault(p => p.SubjectId == id).Name;
            ViewBag.SubjectId = id;
            return View(db.AcademicPlan.Where(p => p.SubjectId == id).ToList());
        }

        public ActionResult DeleteAcademicPlanForSubjectId(int subjectId, int studentId, int semesterId)
        {
            AcademicRecord academicRecord = db.AcademicPlan.FirstOrDefault(p => p.SubjectId == subjectId && p.StudentId == studentId && p.SemesterId == semesterId);
            db.AcademicPlan.Remove(academicRecord);
            db.SaveChanges();
            return RedirectToAction("AcademicPlanForSubjectId", new { id = subjectId });
        }

        public ActionResult CreateAcademicPlanForSubjectId(int id)
        {
            ViewBag.SubjectName = db.Subjects.FirstOrDefault(p => p.SubjectId == id).Name;
            AcademicRecord academicRecord = new AcademicRecord();
            academicRecord.SubjectId = id;
            return View(academicRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAcademicPlanForSubjectId(AcademicRecord academicRecord)
        {
            try
            {
                db.AcademicPlan.Add(academicRecord);
                db.SaveChanges();
                return RedirectToAction("AcademicPlanForSubjectId", new { id = academicRecord.SubjectId });
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
    }
}