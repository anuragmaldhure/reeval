using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Exam.Data;
using Online_Exam.DTOs;
using Online_Exam.Models;
using Online_Exam.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Exam.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly Online_ExamContext _context;
        private readonly IMapper _mapper;

        // Correct the constructor to inject the IMapper instance properly
        public ReportRepository(Online_ExamContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;  // Correct initialization of AutoMapper
        }

        // 1. Get Tests Per Day
        public async Task<IEnumerable<ExamResult>> GetTestsPerDay()
        {
            return await _context.ExamResults
                .Include(er => er.Exam)
                .ToListAsync();
        }

        // 2. Get Tests Finished Before Time by 20%
        public async Task<IEnumerable<ReportTwoDto>> GetTestsFinishedBeforeTime()
        {
            var results = await _context.ExamResults
                .Include(er => er.Exam)
                .Include(er => er.User) // Include User to access UserName and Email
                .Where(er => er.Duration < (er.Exam.Duration * 0.8)) // Less than 80% of the total time
                .ToListAsync();

            var resultDtos = _mapper.Map<IEnumerable<ReportTwoDto>>(results);
            return resultDtos;
        }



        // 3. Get Auto-Submitted Tests After 30 Minutes and Less than 50% of the Questions Attempted
        public async Task<IEnumerable<ReportThreeDto>> GetAutoSubmittedTests()
        {
            var results = await _context.ExamResults
                .Include(er => er.Exam)
                .Include(er => er.User) // Include User to access UserName and Email
                .Include(er => er.UserAnswers)
                .Include(er => er.Exam.Sections)
                .ThenInclude(s => s.Questions)
                .Where(er => er.Duration >= 30  // Auto-submitted after 30 minutes
                             && er.UserAnswers.Count < (er.Exam.Sections.SelectMany(s => s.Questions).Count() / 2)) // Less than 50% of questions attempted
                .ToListAsync();

            var resultDtos = _mapper.Map<IEnumerable<ReportThreeDto>>(results);
            return resultDtos;
        }



        // 4. Get Tests Marked for Review (where the user marked at least 50% of questions for review)
        public async Task<IEnumerable<ExamResultDto>> GetMarkedForReviewTests()
        {
            var results = await _context.ExamResults
                .Include(er => er.Exam)
                .ThenInclude(e => e.Sections)
                .ThenInclude(s => s.Questions)
                .Include(er => er.User) // Include user information
                .Where(er => er.Exam.Sections.SelectMany(s => s.Questions).Count() > 0
                             && er.markforreview >= (er.Exam.Sections.SelectMany(s => s.Questions).Count() * 0.5))
                .ToListAsync();

            // Use AutoMapper to map from ExamResult to ExamResultDto
            var resultDtos = _mapper.Map<IEnumerable<ExamResultDto>>(results);

            return resultDtos;
        }
    }
}
