using Microsoft.EntityFrameworkCore;
using Online_Exam.Data;
using Online_Exam.Models;
using Online_Exam.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Exam.Repositories
{
    public class ExamResultRepository : IExamResultRepository
    {
        private readonly Online_ExamContext _context;

        public ExamResultRepository(Online_ExamContext context)
        {
            _context = context;
        }

        public async Task<ExamResult> SubmitExamResultAsync(ExamResult examResult)
        {
            await _context.ExamResults.AddAsync(examResult);
            await _context.SaveChangesAsync();
            return examResult;
        }

        public async Task AddUserAnswersAsync(IEnumerable<UserAnswer> userAnswers)
        {
            await _context.UserAnswers.AddRangeAsync(userAnswers);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLatestAttemptNumberAsync(string userId, int examId)
        {
            var latestAttempt = await _context.ExamResults
                .Where(er => er.UserId == userId && er.ExamId == examId)
                .OrderByDescending(er => er.AttemptNumber)
                .FirstOrDefaultAsync();

            return latestAttempt?.AttemptNumber ?? 0;  // Return 0 if no previous attempts exist
        }

        // New method to get all exam results
        public async Task<IEnumerable<ExamResult>> GetAllResultsAsync()
        {
            return await _context.ExamResults
                .Include(er => er.Exam)  // Optional: Include related exam data
                .Include(er => er.User)  // Optional: Include related user data
                .ToListAsync();
        }

        // New method to get exam results by UserId
        public async Task<IEnumerable<ExamResult>> GetResultsByUserIdAsync(string userId)
        {
            return await _context.ExamResults
                .Where(er => er.UserId == userId)
                .Include(er => er.Exam)  // Optional: Include related exam data
                .ToListAsync();
        }

        public async Task AddSectionResultsAsync(IEnumerable<SectionResult> sectionResults)
        {
            _context.SectionResults.AddRange(sectionResults);
            await _context.SaveChangesAsync();
        }

    }
}
