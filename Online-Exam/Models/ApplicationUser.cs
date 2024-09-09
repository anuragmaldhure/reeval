using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Online_Exam.Models
{
    public class Online_ExamUser : IdentityUser
    {
        // Navigation properties
        public ICollection<Exam> ExamsCreated { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }
    }
}
