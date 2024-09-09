using System;
using System.Collections.Generic;

namespace Online_Exam.Models
{
    public class Section
    {
        public int SectionId { get; set; }  // Integer-based ID
        public int ExamId { get; set; }  // Foreign Key to Exam
        public string Title { get; set; }  // Section title (e.g., Math, English)
        public int NumberOfQuestions { get; set; }  // Number of questions in this section
        
        public int TotalMarks { get; set; }  // Total marks for this section

        public int passingMarks { get; set; }  // Passing marks for this section
        // Navigation properties
        public Exam Exam { get; set; }
        public decimal? Weightage { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<SectionResult> SectionResults { get; set; }
    }
}
