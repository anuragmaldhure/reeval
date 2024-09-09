using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;

namespace Online_Exam.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int SectionId { get; set; }
        public string QuestionText { get; set; }
        public bool IsMultipleChoice { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation property
        public Section Section { get; set; }

        public bool? HasDifferentialMarking { get; set; }
        public MediaType? mediaType { get; set; }
        public string? MediaUrl { get; set; }
        public ICollection<Option> Options { get; set; }

        // Add this navigation property to fix the error
        public ICollection<UserAnswer> UserAnswers { get; set; }

    }


    public enum MediaType
    {
        None,
        Image,
        Video
    }
}
