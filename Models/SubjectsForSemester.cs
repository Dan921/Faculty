using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class SubjectsForSemester
    {
        public int SubjectId { get; set; }
        public int SemesterId { get; set; }
        public int Lecture_scope { get; set; }
        public int Practice_scope { get; set; }
        public int laboratory_work_scope { get; set; }
    }
}
