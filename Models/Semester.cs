using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class Semester
    {
        [Key]
        public int SemesterId { get; set; }
        public int SemesterNumber { get; set; }
    }
}
