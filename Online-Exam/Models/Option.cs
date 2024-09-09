using System.Collections.Generic;

namespace Online_Exam.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }

        public int? Marks { get; set; }

        // Navigation property
        public Question Question { get; set; }

        // Add this navigation property to fix the error
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
