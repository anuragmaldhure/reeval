using System.Collections.Generic;
using Online_Exam.DTOs;
namespace Online_Exam.DTOs
{
    public class CreateSectionDto
    {
        public int ExamId { get; set; }  // Foreign key to Exam
        public string Title { get; set; }
        public int TotalMarks { get; set; }
    }

    public class UpdateSectionDto
    {
        public string Title { get; set; }
        public int TotalMarks { get; set; }
    }

    public class SectionDto
    {
        public int SectionId { get; set; }
        public int ExamId { get; set; }
        public string Title { get; set; }
        public int TotalMarks { get; set; }
        public int NumberOfQuestions { get; set; }  
        public int passingMarks { get; set; }
        public decimal Weightage { get; set; }
        public IEnumerable<Online_Exam.DTOs.QuestionDto> Questions { get; set; }
        
    }

  
}