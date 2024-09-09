using Microsoft.AspNetCore.Mvc;
using Online_Exam.Repositories.Interfaces;
using Online_Exam.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Online_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        // 1. Number of tests taken per day
        [HttpGet("TestsPerDay")]
        public async Task<IActionResult> GetTestsPerDay()
        {
            var results = await _reportRepository.GetTestsPerDay();
            var report = results
                .GroupBy(r => r.CompletedDate.Date)
                .Select(g => new { Date = g.Key, NumberOfTests = g.Count() })
                .ToList();

            return Ok(report);
        }

        // 2. Tests finished before time by 20%
        [HttpGet("FinishedBeforeTime")]
        public async Task<IActionResult> GetTestsFinishedBeforeTime()
        {
            var results = await _reportRepository.GetTestsFinishedBeforeTime();
            return Ok(results);
        }

        // 3. Tests auto-submitted after 30 minutes and less than 50% of the questions attempted
        [HttpGet("AutoSubmittedAfter30Mins")]
        public async Task<IActionResult> GetTestsAutoSubmittedAfter30Mins()
        {
            var results = await _reportRepository.GetAutoSubmittedTests();
            return Ok(results);
        }

        // 4. Tests where user marked at least 50% or more questions as "mark for review"
        [HttpGet("MarkedForReview")]
        public async Task<IActionResult> GetTestsMarkedForReview()
        {
            var results = await _reportRepository.GetMarkedForReviewTests();
            return Ok(results);
        }

        //5.1 
        [HttpGet("QuestionsWithImages")]
        public async Task<IActionResult> GetQuestionsWithImages()
        {
            var results = await _reportRepository.GetQuestionsWithImages();
            return Ok(results);
        }
        //5.2
        [HttpGet("QuestionsWithVideos")]
        public async Task<IActionResult> GetQuestionsWithVideos()
        {
            var results = await _reportRepository.GetQuestionsWithVideos();
            return Ok(results);
        }

        //5.3 API to get the count of questions with images
        [HttpGet("CountImageQuestions")]
        public async Task<IActionResult> GetImageQuestionsCount()
        {
            var count = await _reportRepository.GetImageQuestionsCount();
            return Ok(new { ImageQuestionsCount = count });
        }

        //5.4 API to get the count of questions with videos
        [HttpGet("CountVideoQuestions")]
        public async Task<IActionResult> GetVideoQuestionsCount()
        {
            var count = await _reportRepository.GetVideoQuestionsCount();
            return Ok(new { VideoQuestionsCount = count });
        }

        //6.0

        [HttpGet("Top10Students/{examId}")]
        public async Task<IActionResult> GetTop10StudentsByPercentile(int examId)
        {
            var top10Students = await _reportRepository.GetTop10StudentsByPercentile(examId);

            // If there are no exam results found, return a 404 with an appropriate message
            if (top10Students == null)
            {
                return NotFound($"No exam results found for exam ID: {examId}.");
            }

            // If results are available, return 200 OK
            return Ok(top10Students);
        }



    }
}




// 4. Tests where user marked at least 50% or more questions as "mark for review"
/*[HttpGet("MarkedForReview")]
public async Task<IActionResult> GetTestsMarkedForReview()
{
    var results = await _examResultRepository.GetAllResultsAsync();

    var report = results
        .Where(r =>
        {
            // Safely check if Exam, Sections, and Questions are not null
            if (r.Exam == null || r.Exam.Sections == null || !r.Exam.Sections.Any())
                return false;

            // Safely check if there are any questions in the sections
            int totalQuestions = r.Exam.Sections.SelectMany(s => s.Questions)?.Count() ?? 0;

            // Ensure there are valid questions in the exam
            if (totalQuestions == 0)
                return false;

            // Check if the number of marked-for-review questions is at least 50% of the total questions
            return r.markforreview >= (10);
        })
        .ToList();

    return Ok(report);
}*/