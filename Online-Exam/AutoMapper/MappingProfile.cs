using AutoMapper;
using Online_Exam.Models;
using Online_Exam.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Online_Exam.DTOs.Online_Exam.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Exam, ExamDto>();
        CreateMap<CreateExamDto, Exam>()
            .ForMember(dest => dest.IsPublished, opt => opt.Ignore()) // Set manually in the controller
            .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedByUserId));
        CreateMap<UpdateExamDto, Exam>();


        CreateMap<Section, SectionDto>();
        CreateMap<CreateSectionDto, Section>();
        CreateMap<UpdateSectionDto, Section>();

        CreateMap<Question, QuestionDto>().ReverseMap();
        CreateMap<CreateQuestionDto, Question>();
        CreateMap<Option, OptionDto>().ReverseMap();
        CreateMap<CreateOptionDto, Option>();

        // UserAnswer mapping
        CreateMap<UserAnswer, UserAnswerDto>()
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
            .ForMember(dest => dest.SelectedOptionIds, opt => opt.MapFrom(src => src.SelectedOptionId))
            .ReverseMap(); // Allows reverse mapping from DTO to entity

        // ExamResult mapping with new fields 
        CreateMap<ExamResult, SubmitExamResultDto>()
            .ForMember(dest => dest.ExamResultId, opt => opt.MapFrom(src => src.ExamResultId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.ExamId))
            .ForMember(dest => dest.AttemptNumber, opt => opt.MapFrom(src => src.AttemptNumber))
            .ForMember(dest => dest.TotalScore, opt => opt.MapFrom(src => src.TotalScore))
            .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.Percentage))
            .ForMember(dest => dest.Passed, opt => opt.MapFrom(src => src.Passed))
            .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
            .ForMember(dest => dest.markforreview, opt => opt.MapFrom(src => src.markforreview))
            .ForMember(dest => dest.SectionResults, opt => opt.MapFrom(src => src.SectionResults))  // New section results mapping
            .ReverseMap();


        // ExamResultDto mapping (No major changes except for ensuring score calculation)
        CreateMap<ExamResult, ExamResultDto>()
            .ForMember(dest => dest.ExamResultId, opt => opt.MapFrom(src => src.ExamResultId))
            .ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.Exam.Title))
            .ForMember(dest => dest.TotalQuestions, opt => opt.MapFrom(src => src.Exam.Sections.SelectMany(s => s.Questions).Count()))
            .ForMember(dest => dest.markforreview, opt => opt.MapFrom(src => src.markforreview))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate))
            .ForMember(dest => dest.TotalScore, opt => opt.MapFrom(src => src.TotalScore))  // Updated score calculation
            .ForMember(dest => dest.Passed, opt => opt.MapFrom(src => src.Passed))
            .ReverseMap();


        CreateMap<ExamResult, ReportTwoDto>()
            .ForMember(dest => dest.ExamResultId, opt => opt.MapFrom(src => src.ExamResultId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)) // Map UserName
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))   // Map UserEmail
            .ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.Exam.Title))
            .ForMember(dest => dest.TotalScore, opt => opt.MapFrom(src => src.TotalScore))
            .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.Percentage))
            .ForMember(dest => dest.Passed, opt => opt.MapFrom(src => src.Passed))
            .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration));

        // Mapping for ReportThreeDto
        CreateMap<ExamResult, ReportThreeDto>()
            .ForMember(dest => dest.ExamResultId, opt => opt.MapFrom(src => src.ExamResultId))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)) // Map UserName
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))   // Map UserEmail
            .ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.Exam.Title))
            .ForMember(dest => dest.TotalScore, opt => opt.MapFrom(src => src.TotalScore))
            .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.Percentage))
            .ForMember(dest => dest.Passed, opt => opt.MapFrom(src => src.Passed))
            .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
            .ForMember(dest => dest.TotalQuestions, opt => opt.MapFrom(src => src.Exam.Sections.SelectMany(s => s.Questions).Count())) // Total questions
            .ForMember(dest => dest.AttemptedQuestions, opt => opt.MapFrom(src => src.UserAnswers.Count())); // Attempted questions

        // Section mappings
        CreateMap<Section, SectionGetDTO>().ReverseMap();
        CreateMap<SectionPostDTO, Section>();

        // Question mappings
        CreateMap<Question, QuestionGetDTO>().ReverseMap();
        CreateMap<QuestionPostDTO, Question>();

        // Option mappings
        CreateMap<Option, OptionGetDTO>().ReverseMap();
        CreateMap<OptionPostDTO, Option>();

        CreateMap<SectionResult, SectionResultDto>().ReverseMap();




    }
}
