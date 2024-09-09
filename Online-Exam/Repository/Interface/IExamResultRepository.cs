using Online_Exam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Exam.Repositories.Interfaces
{
    public interface IExamResultRepository
    {
        Task<ExamResult> SubmitExamResultAsync(ExamResult examResult);
        Task AddUserAnswersAsync(IEnumerable<UserAnswer> userAnswers);
        Task<int> GetLatestAttemptNumberAsync(string userId, int examId);
        Task<IEnumerable<ExamResult>> GetAllResultsAsync();  // New method to get all results
        Task<IEnumerable<ExamResult>> GetResultsByUserIdAsync(string userId);
        Task AddSectionResultsAsync(IEnumerable<SectionResult> sectionResults);
    }
}
