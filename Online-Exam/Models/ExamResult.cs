using System;
using System.Collections.Generic;

namespace Online_Exam.Models
{
    public class ExamResult
    {
        public int ExamResultId { get; set; }  // Integer-based ID
        public string UserId { get; set; }  // Foreign Key to ApplicationUser
        public int ExamId { get; set; }  // Foreign Key to Exam
        public int AttemptNumber { get; set; }
        public int TotalScore { get; set; }
        public double Percentage { get; set; }
        public bool Passed { get; set; }
        public DateTime CompletedDate { get; set; }
        public int Duration { get; set; }  // Duration in minutes
        public int markforreview { get; set; }

        // Navigation properties
        public Online_ExamUser User { get; set; }
        public Exam Exam { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
        public ICollection<SectionResult> SectionResults { get; set; }
    }
}
