namespace Online_Exam.Models
{
    public class SectionResult
    {
        public int SectionResultId { get; set; }
        public int SectionId { get; set; }  // Foreign Key to Section
        public int ExamResultId { get; set; }  // Foreign Key to ExamResult
        public int AttemptedQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double SectionScore { get; set; }
        public bool IsPassed { get; set; }

        // Navigation properties
        public Section Section { get; set; }
        public ExamResult ExamResult { get; set; }
    }
}
