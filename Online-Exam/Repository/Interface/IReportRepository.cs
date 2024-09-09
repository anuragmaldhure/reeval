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

        //questions with images
        Task<IEnumerable<QuestionImageReportDto>> GetQuestionsWithImages();

        //questions with videos
        Task<IEnumerable<QuestionVideoReportDto>> GetQuestionsWithVideos();

        Task<int> GetImageQuestionsCount();
        Task<int> GetVideoQuestionsCount();

        Task<IEnumerable<TopStudentDto>> GetTop10StudentsByPercentile(int examId);
    }
}