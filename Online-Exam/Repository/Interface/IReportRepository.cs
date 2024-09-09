using Online_Exam.DTOs;
using Online_Exam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Exam.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<ExamResult>> GetTestsPerDay();
        Task<IEnumerable<ReportTwoDto>> GetTestsFinishedBeforeTime();
        Task<IEnumerable<ReportThreeDto>> GetAutoSubmittedTests();
        Task<IEnumerable<ExamResultDto>> GetMarkedForReviewTests();
    }
}
