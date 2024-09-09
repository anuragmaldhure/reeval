namespace Online_Exam.DTOs
{
    namespace Online_Exam.DTOs
    {
        public class CreateOptionDto
        {
            public string OptionText { get; set; }
            public bool IsCorrect { get; set; }
            public int Marks { get; set; }
        }

        public class CreateQuestionDto
        {
            public int SectionId { get; set; }
            public string QuestionText { get; set; }
            public bool IsMultipleChoice { get; set; }
            public bool HasDifferentialMarking { get; set; }
            public MediaType mediaType { get; set; }
            public string MediaUrl { get; set; }
            public List<CreateOptionDto> Options { get; set; }
        }

        public class OptionDto
        {
            public int OptionId { get; set; }
            public string OptionText { get; set; }
            public bool IsCorrect { get; set; }
            public int Marks { get; set; }
        }

        public class QuestionDto
        {
            public int QuestionId { get; set; }
            public int SectionId { get; set; }
            public string QuestionText { get; set; }
            public bool IsMultipleChoice { get; set; }
            public bool HasDifferentialMarking { get; set; }
            public MediaType mediaType { get; set; }
            public string MediaUrl { get; set; }
            public List<OptionDto> Options { get; set; }
        }

        public class QuestionWithImageDto
        {
            public int QuestionId { get; set; }
            public string QuestionText { get; set; }
            public string SectionTitle { get; set; }
            public string MediaUrl { get; set; }
        }

    }

}