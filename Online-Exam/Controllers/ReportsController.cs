using Microsoft.AspNetCore.Mvc;
using Online_Exam.Repositories.Interfaces;
using Online_Exam.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

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


