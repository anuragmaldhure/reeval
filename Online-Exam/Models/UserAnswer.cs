namespace Online_Exam.Models
{
    public class UserAnswer
    {
        public int UserAnswerId { get; set; }
        public int ResultId { get; set; }  // Foreign Key to ExamResult
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }

        // Navigation properties
        public ExamResult ExamResult { get; set; }
        public Question Question { get; set; }
        public Option SelectedOption { get; set; }
    }
}
