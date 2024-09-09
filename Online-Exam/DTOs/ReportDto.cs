namespace Online_Exam.DTOs
{
    public class ReportTwoDto
    {
        public int ExamResultId { get; set; }
        public string UserName { get; set; }  // Replace UserId with UserName
        public string UserEmail { get; set; } // Add UserEmail
        public string ExamTitle { get; set; }
        public int TotalScore { get; set; }
        public double Percentage { get; set; }
        public bool Passed { get; set; }
        public DateTime CompletedDate { get; set; }
        public int Duration { get; set; }
    }
    public class ReportThreeDto
    {
        public int ExamResultId { get; set; }
        public string UserName { get; set; }  // Replace UserId with UserName
        public string UserEmail { get; set; } // Add UserEmail
        public string ExamTitle { get; set; }
        public int TotalScore { get; set; }
        public double Percentage { get; set; }
        public bool Passed { get; set; }
        public DateTime CompletedDate { get; set; }
        public int Duration { get; set; }
        public int TotalQuestions { get; set; }
        public int AttemptedQuestions { get; set; }
    }

    public class QuestionImageReportDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Title { get; set; }
        public string MediaUrl { get; set; }
    }

    public class QuestionVideoReportDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Title { get; set; }
        public string MediaUrl { get; set; }
    }

    public class TopStudentDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int TotalScore { get; set; }

        public int AttemptNumber { get; set; }
        public double Percentile { get; set; }
    }


}