using Online_Exam.Models;

namespace Online_Exam.Repository.Interface
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetAllExamsAsync();
        Task<Exam> GetExamByIdAsync(int examId);
        Task CreateExamAsync(Exam exam);
        Task UpdateExamAsync(Exam exam);
        Task DeleteExamAsync(int examId);
    }

}
