using Online_Exam.DTOs;

namespace Online_Exam.Repository.Interface
{
    public interface ISectionActionRepository
    {
        Task<IEnumerable<SectionGetDTO>> GetSectionsByExamIdAsync(int examId);
        Task<SectionGetDTO> GetSectionByIdAsync(int sectionId);
        Task CreateSectionAsync(SectionPostDTO sectionDto);
        Task UpdateSectionAsync(int sectionId, SectionPostDTO sectionDto);
        Task DeleteSectionAsync(int sectionId);

        Task AddQuestionAsync(int sectionId, QuestionPostDTO questionDto);
        Task DeleteQuestionAsync(int questionId);
    }


}
