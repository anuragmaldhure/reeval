using System;
using System.Collections.Generic;

namespace Online_Exam.Models
{
    public class Exam
    {
        public int ExamId { get; set; }  // Integer-based ID
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedByUserId { get; set; }  // Foreign Key to ApplicationUser
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPublished { get; set; }

        // Additional fields
        public int Duration { get; set; }  // Duration in minutes
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }
        public bool isRandmized { get; set; }

        // Navigation properties
        public Online_ExamUser CreatedByUser { get; set; }
        public ICollection<Section> Sections { get; set; }
        // Add this navigation property for ExamResults
        public ICollection<ExamResult> ExamResults { get; set; }
    }
}
