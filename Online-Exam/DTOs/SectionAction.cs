using System;
using System.Collections.Generic;

namespace Online_Exam.DTOs
{
    // Option DTOs
    public class OptionPostDTO
    {
        public string OptionText { get; set; }   // Text for the option
        public bool IsCorrect { get; set; }      // Indicates if the option is correct
        public int Marks { get; set; }
    }

        public class OptionGetDTO
        {
            public int OptionId { get; set; }        // Option identifier
            public string OptionText { get; set; }   // Text for the option
            public bool IsCorrect { get; set; }      // Indicates if the option is correct
        public int Marks { get; set; }


    }
    // Question DTOs
    public class QuestionPostDTO
    {
        public string QuestionText { get; set; }   // The question text
        public bool IsMultipleChoice { get; set; }       // Indicates if the question allows multiple answers
        public DateTime CreatedDate { get; set; }  // Date the question was created
        public bool HasDifferentialMarking { get; set; }
        public MediaType mediaType { get; set; }
        public string MediaUrl { get; set; }

        public List<OptionPostDTO> Options { get; set; }   // List of options for the question
    }

    public class QuestionGetDTO
    {
        public int QuestionId { get; set; }        // Question identifier
        public string QuestionText { get; set; }   // The question text
        public bool IsMultiple { get; set; }       // Indicates if the question allows multiple answers
        public DateTime CreatedDate { get; set; }  // Date the question was created
        public bool HasDifferentialMarking { get; set; }
        public MediaType mediaType { get; set; }
        public string MediaUrl { get; set; }

        public List<OptionGetDTO> Options { get; set; }   // List of options for the question
    }

    // Section DTOs
    public class SectionPostDTO
    {
        public int ExamId { get; set; }              // Foreign key to Exam
        public string Title { get; set; }            // Section title (e.g., Math, Science)
        public int NumberOfQuestions { get; set; }   // Number of questions in the section
        public int TotalMarks { get; set; }          // Total marks for the section
        public int PassingMarks { get; set; }        // Minimum marks required to pass
        public decimal Weightage { get; set; }
        public List<QuestionPostDTO> Questions { get; set; }   // List of questions in the section
    }

    public class SectionGetDTO
    {
        public int SectionId { get; set; }           // Section identifier
        public int ExamId { get; set; }              // Foreign key to Exam
        public string Title { get; set; }            // Section title
        public int NumberOfQuestions { get; set; }   // Number of questions in the section
        public int TotalMarks { get; set; }          // Total marks for the section
        public int PassingMarks { get; set; }        // Minimum marks required to pass
        public decimal Weightage { get; set; }
        public List<QuestionGetDTO> Questions { get; set; }   // List of questions in the section
    }

    public enum MediaType
    {
        None,
        Image,
        Video
    }
}
