using Online_Exam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Exam.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task CreateQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int questionId);
        Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId);
        Task<IEnumerable<Section>> GetSectionsWithQuestionsAsync(int examId);
    }
}
