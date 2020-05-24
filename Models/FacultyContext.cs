using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class FacultyContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<AcademicRecord> AcademicPlan { get; set; }
        public DbSet<SubjectsForSemester> Semesters_subjects { get; set; }

        public FacultyContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;UserId=admin;Password=12345;database=faculty;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicRecord>().HasKey(c => new { c.StudentId, c.SubjectId, c.SemesterId });
            modelBuilder.Entity<SubjectsForSemester>().HasKey(c => new { c.SubjectId, c.SemesterId });
        }
    }
}
