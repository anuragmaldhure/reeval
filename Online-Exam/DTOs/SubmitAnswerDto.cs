using System;
using System.Collections.Generic;

namespace Online_Exam.DTOs
{
    public class SubmitResultDto
    {
        public int ExamId { get; set; }  // Foreign Key to Exam
        public string UserId { get; set; }  // Foreign Key to ApplicationUser
        public int Duration { get; set; }  // Duration in minutes
        public int markforreview { get; set; }
        public List<UserAnswerDto> UserAnswers { get; set; }
    }

    public class UserAnswerDto
    {
        public int QuestionId { get; set; }
        public List<int> SelectedOptionIds { get; set; }
    }
    public class SubmitExamResultDto
    {
        public int ExamResultId { get; set; }
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public int AttemptNumber { get; set; }
        public int TotalScore { get; set; }
        public double Percentage { get; set; }
        public bool Passed { get; set; }
        public int Duration { get; set; }  // Duration in minutes
        public int markforreview { get; set; }  // Added field
        public DateTime CompletedDate { get; set; }
        public List<SectionResultDto> SectionResults { get; set; }
    }
    public class ExamResultDto
    {
        public int ExamResultId { get; set; }
        public int markforreview { get; set; }
        public int TotalQuestions { get; set; }
        public string ExamTitle { get; set; }

        // New fields for user information
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        // Additional exam result fields
        public DateTime CompletedDate { get; set; }
        public int TotalScore { get; set; }
        public bool Passed { get; set; }
    }
    public class SectionResultDto
    {
        public int SectionId { get; set; }
        public int AttemptedQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double SectionScore { get; set; }  // Calculated score for the section
        public bool IsPassed { get; set; }  // Whether the user passed the section or not
    }


}
