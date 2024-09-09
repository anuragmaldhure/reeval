using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Exam.Data;
using Online_Exam.DTOs;
using Online_Exam.Models;
using Online_Exam.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaType = Online_Exam.Models.MediaType;

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
                .Where(er => er.Duration <= (er.Exam.Duration * 0.8)) // Less than or equal 80% of the total time
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
                .Where(er => er.Duration >= er.Exam.Duration  // Auto-submitted 
                             && er.UserAnswers.Count < (er.Exam.Sections.SelectMany(s => s.Questions).Count() / 2)) // Less than 50% of questions attempted
                .ToListAsync();

            var resultDtos = _mapper.Map<IEnumerable<ReportThreeDto>>(results);
            return resultDtos;
        }



        // 4. Get Tests Marked for Review (where the user marked at least 50% of questions for review)
        public async Task<IEnumerable<ExamResultDto>> GetMarkedForReviewTests()
        {
            // First, get all exam results with their related exam sections and questions
            var results = await _context.ExamResults
                .Include(er => er.Exam)
                .ThenInclude(e => e.Sections)
                .ThenInclude(s => s.Questions)
                .Include(er => er.User) // Include user information
                .ToListAsync();

            // Filter results to only those where at least 50% of questions are marked for review
            var filteredResults = results
                .Where(er =>
                {
                    // Calculate the total number of questions in the exam
                    var totalQuestions = er.Exam.Sections.Sum(s => s.NumberOfQuestions);

                    // Calculate the percentage of questions marked for review
                    var markedForReview = er.markforreview;

                    return totalQuestions > 0 && markedForReview >= (totalQuestions * 0.5);
                });

            // Use AutoMapper to map from ExamResult to ExamResultDto
            var resultDtos = _mapper.Map<IEnumerable<ExamResultDto>>(filteredResults);

            return resultDtos;
        }




        // 5.1 Get Questions with Images and Their Sections
        public async Task<IEnumerable<QuestionImageReportDto>> GetQuestionsWithImages()
        {
            var questionsWithImages = await _context.Questions
                .Include(q => q.Section)
                .Where(q => q.mediaType == MediaType.Image) // Filter for questions with images
                .Select(q => new QuestionImageReportDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    Title = q.Section.Title,
                    MediaUrl = q.MediaUrl
                })
                .ToListAsync();

            return questionsWithImages;
        }

        // 5.2 Get Questions with Videos and Their Sections
        public async Task<IEnumerable<QuestionVideoReportDto>> GetQuestionsWithVideos()
        {
            var questionsWithVideos = await _context.Questions
                .Include(q => q.Section)
                .Where(q => q.mediaType == MediaType.Video) // Filter for questions with videos
                .Select(q => new QuestionVideoReportDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    Title = q.Section.Title,
                    MediaUrl = q.MediaUrl
                })
                .ToListAsync();

            return questionsWithVideos;
        }

        // 5.3 Get count of questions with images
        public async Task<int> GetImageQuestionsCount()
        {
            var imageQuestionsCount = await _context.Questions
                .Where(q => q.mediaType == MediaType.Image) // Filter for questions with images
                .CountAsync(); // Get the count

            return imageQuestionsCount;
        }

        // 5.4 Get count of questions with videos
        public async Task<int> GetVideoQuestionsCount()
        {
            var videoQuestionsCount = await _context.Questions
                .Where(q => q.mediaType == MediaType.Video) // Filter for questions with videos
                .CountAsync(); // Get the count

            return videoQuestionsCount;
        }


        public async Task<IEnumerable<TopStudentDto>> GetTop10StudentsByPercentile(int examId)
        {
            // Get all exam results for a specific exam
            var examResults = await _context.ExamResults
                .Where(er => er.ExamId == examId)
                .OrderByDescending(er => er.TotalScore)
                .Include(er => er.User)  // Include user information for UserName and Email
                .ToListAsync();

            // If there are no results, return an empty list or null
            if (!examResults.Any())
            {
                return null;  // Handle this scenario in the controller
            }

            // Get the highest score from the exam results
            var topScore = examResults.Max(er => er.TotalScore);

            // If no scores are available, return an empty list
            if (topScore == 0) return Enumerable.Empty<TopStudentDto>();

            // Calculate the percentile for each student and select top 10 students
            var top10Students = examResults
                .Take(10)  // Select the top 10 students by score
                .Select(er => new TopStudentDto
                {
                    UserName = er.User.UserName,
                    Email = er.User.Email,
                    TotalScore = er.TotalScore,
                    AttemptNumber = er.AttemptNumber,
                    Percentile = (er.TotalScore / (double)topScore) * 100 // Calculate the percentile relative to the top score
                })
                .ToList();

            return top10Students;
        }



    }
}