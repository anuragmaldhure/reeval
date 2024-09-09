using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Exam.DTOs;
using Online_Exam.Models;
using Online_Exam.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;


namespace Online_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultController : ControllerBase
    {
        private readonly IExamResultRepository _examResultRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public ExamResultController(IExamResultRepository examResultRepository, IQuestionRepository questionRepository, IMapper mapper)
        {
            _examResultRepository = examResultRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }





        /*[HttpPost("Submit")]
        public async Task<IActionResult> SubmitResult([FromBody] SubmitResultDto submitResultDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Retrieve sections and questions for the exam
            var sections = await _questionRepository.GetSectionsWithQuestionsAsync(submitResultDto.ExamId);

            int totalObtainedMarks = 0;
            var userAnswers = new List<UserAnswer>();
            var sectionResults = new List<SectionResult>();

            foreach (var section in sections)
            {
                int sectionObtainedMarks = 0;
                int attemptedQuestions = 0;
                int correctAnswers = 0;

                foreach (var question in section.Questions)
                {
                    var userAnswerDto = submitResultDto.UserAnswers.FirstOrDefault(ua => ua.QuestionId == question.QuestionId);
                    if (userAnswerDto == null) continue;

                    attemptedQuestions++;

                    var isCorrect = question.Options.Any(o => o.OptionId == userAnswerDto.SelectedOptionId && o.IsCorrect);
                    if (isCorrect)
                    {
                        correctAnswers++;
                        sectionObtainedMarks += 1; // Assuming each correct answer gives 1 mark
                    }

                    // Store user answer
                    userAnswers.Add(new UserAnswer
                    {
                        QuestionId = userAnswerDto.QuestionId,
                        SelectedOptionId = userAnswerDto.SelectedOptionId,
                        ResultId = 0  // This will be updated after saving the result
                    });
                }

                // Calculate the section score and pass status
                var sectionScore = (double)sectionObtainedMarks / section.NumberOfQuestions * section.TotalMarks;
                var isPassed = sectionScore >= section.passingMarks;

                // Store section result
                sectionResults.Add(new SectionResult
                {
                    SectionId = section.SectionId,
                    AttemptedQuestions = attemptedQuestions,
                    CorrectAnswers = correctAnswers,
                    SectionScore = sectionScore,
                    IsPassed = isPassed
                });

                totalObtainedMarks += sectionObtainedMarks;
            }

            // Save the overall exam result
            var percentage = (double)totalObtainedMarks / sections.Sum(s => s.TotalMarks) * 100;
            var passed = percentage >= 50; // Assuming 50% is the passing mark
            var latestAttemptNumber = await _examResultRepository.GetLatestAttemptNumberAsync(submitResultDto.UserId, submitResultDto.ExamId);

            var examResult = new ExamResult
            {
                UserId = submitResultDto.UserId,
                ExamId = submitResultDto.ExamId,
                AttemptNumber = latestAttemptNumber + 1,
                TotalScore = totalObtainedMarks,
                Percentage = percentage,
                Passed = passed,
                CompletedDate = DateTime.UtcNow,
                Duration = submitResultDto.Duration,
                markforreview = submitResultDto.markforreview
            };

            var savedExamResult = await _examResultRepository.SubmitExamResultAsync(examResult);

            // Update ResultId in SectionResult and UserAnswers and save them
            sectionResults.ForEach(sr => sr.ExamResultId = savedExamResult.ExamResultId);
            await _examResultRepository.AddSectionResultsAsync(sectionResults);

            userAnswers.ForEach(ua => ua.ResultId = savedExamResult.ExamResultId);
            await _examResultRepository.AddUserAnswersAsync(userAnswers);

            var examResultDto = _mapper.Map<SubmitExamResultDto>(savedExamResult);
            return Ok(examResultDto);
        }*/

        [HttpPost("Submit")]
        public async Task<IActionResult> SubmitResult([FromBody] SubmitResultDto submitResultDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Console.WriteLine("Start processing the exam submission.");

            // Retrieve sections and questions for the exam
            var sections = await _questionRepository.GetSectionsWithQuestionsAsync(submitResultDto.ExamId);

            decimal totalObtainedMarks = 0;  // Use decimal for internal calculations
            var userAnswers = new List<UserAnswer>();
            var sectionResults = new List<SectionResult>();

            foreach (var section in sections)
            {
                decimal sectionObtainedMarks = 0;  // Use decimal for section score calculation
                int attemptedQuestions = 0;
                int correctAnswers = 0;

                Console.WriteLine($"Processing section: {section.Title} (Weightage: {section.Weightage})");

                foreach (var question in section.Questions)
                {
                    var userAnswerDto = submitResultDto.UserAnswers.FirstOrDefault(ua => ua.QuestionId == question.QuestionId);
                    if (userAnswerDto == null)
                    {
                        Console.WriteLine($"No answer provided for question: {question.QuestionId}");
                        continue;
                    }

                    attemptedQuestions++;

                    // Get options with marks for the question
                    var options = await _questionRepository.GetOptionsByQuestionIdAsync(question.QuestionId);
                    decimal positiveMarks = 0;
                    decimal negativeMarks = 0;

                    foreach (var option in options)
                    {
                        if (userAnswerDto.SelectedOptionIds.Contains(option.OptionId))
                        {
                            if (option.IsCorrect)
                            {
                                positiveMarks += option.Marks ?? 0;  // Add positive marks for correct answer
                                correctAnswers++;
                                Console.WriteLine($"Correct option selected: {option.OptionId}, Marks: {option.Marks}");
                            }
                            else
                            {
                                // Apply section weightage to negative marks
                                negativeMarks += (option.Marks ?? 0) * (section.Weightage ?? 1.0M);
                                Console.WriteLine($"Incorrect option selected: {option.OptionId}, Negative Marks: {option.Marks} (Weighted: {negativeMarks})");
                            }
                        }
                    }

                    // Total for this question: positive + weighted negative
                    var questionScore = positiveMarks + negativeMarks;
                    sectionObtainedMarks += questionScore;  // Add to section total
                    Console.WriteLine($"Total score for question {question.QuestionId}: {questionScore}");
                }

                // Calculate section score and determine if passed
                var sectionScore = sectionObtainedMarks ;
                var isPassed = sectionScore >= section.passingMarks;

                // Store section result (rounded and converted to int)
                sectionResults.Add(new SectionResult
                {
                    SectionId = section.SectionId,
                    AttemptedQuestions = attemptedQuestions,
                    CorrectAnswers = correctAnswers,
                    SectionScore = (int)Math.Round(sectionScore),  // Convert section score to int
                    IsPassed = isPassed
                });

                totalObtainedMarks += sectionObtainedMarks;
                Console.WriteLine($"Total obtained marks for section {section.SectionId}: {sectionObtainedMarks}");
            }

            // Calculate percentage
            var percentage = totalObtainedMarks / sections.Sum(s => s.TotalMarks) * 100;
            var passed = percentage >= 50;  // Assume 50% is passing
            Console.WriteLine($"Total obtained marks: {totalObtainedMarks}, Percentage: {percentage}");

            var latestAttemptNumber = await _examResultRepository.GetLatestAttemptNumberAsync(submitResultDto.UserId, submitResultDto.ExamId);

            // Save the overall exam result (round and convert to int)
            var examResult = new ExamResult
            {
                UserId = submitResultDto.UserId,
                ExamId = submitResultDto.ExamId,
                AttemptNumber = latestAttemptNumber + 1,
                TotalScore = (int)Math.Round(totalObtainedMarks),  // Convert total score to int
                Percentage = (double)percentage,  // Still decimal for percentage but may need rounding if displayed
                Passed = passed,
                CompletedDate = DateTime.UtcNow,
                Duration = submitResultDto.Duration,
                markforreview = submitResultDto.markforreview
            };

            var savedExamResult = await _examResultRepository.SubmitExamResultAsync(examResult);
            Console.WriteLine($"Saved exam result with ID: {savedExamResult.ExamResultId}");

            // Update SectionResults and UserAnswers
            sectionResults.ForEach(sr => sr.ExamResultId = savedExamResult.ExamResultId);
            await _examResultRepository.AddSectionResultsAsync(sectionResults);
            Console.WriteLine("Section results saved.");

            userAnswers.ForEach(ua => ua.ResultId = savedExamResult.ExamResultId);
            await _examResultRepository.AddUserAnswersAsync(userAnswers);
            Console.WriteLine("User answers saved.");

            var examResultDto = _mapper.Map<SubmitExamResultDto>(savedExamResult);
            Console.WriteLine("Exam submission process completed.");

            return Ok(examResultDto);
        }






        // GET: api/ExamResult/GetAllResults
        [HttpGet("GetAllResults")]
        public async Task<IActionResult> GetAllResults()
        {
            var examResults = await _examResultRepository.GetAllResultsAsync();
            var resultDtos = _mapper.Map<IEnumerable<SubmitExamResultDto>>(examResults);
            return Ok(resultDtos);
        }

        // GET: api/ExamResult/GetResultsByUser/{userId}
        [HttpGet("GetResultsByUser/{userId}")]
        public async Task<IActionResult> GetResultsByUser(string userId)
        {
            var examResults = await _examResultRepository.GetResultsByUserIdAsync(userId);
            if (examResults == null || !examResults.Any())
                return NotFound("No results found for the given user.");

            var resultDtos = _mapper.Map<IEnumerable<SubmitExamResultDto>>(examResults);
            return Ok(resultDtos);
        }
    }
}



/*// POST: api/ExamResult/Submit
        [HttpPost("Submit")]
        public async Task<IActionResult> SubmitResult([FromBody] SubmitResultDto submitResultDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Retrieve questions for the exam
            var questions = await _questionRepository.GetQuestionsByExamIdAsync(submitResultDto.ExamId);

            // Total number of questions in the exam
            int totalQuestions = questions.Count();

            int obtainedMarks = 0;
            var userAnswers = new List<UserAnswer>();

            foreach (var userAnswerDto in submitResultDto.UserAnswers)
            {
                var question = questions.FirstOrDefault(q => q.QuestionId == userAnswerDto.QuestionId);
                if (question == null) continue;

                // Check if the user's selected option is correct
                var isCorrect = question.Options.Any(o => o.OptionId == userAnswerDto.SelectedOptionId && o.IsCorrect);

                if (isCorrect)
                {
                    obtainedMarks += 1;  // Assuming each correct answer is worth 1 mark
                }

                // Prepare UserAnswer entity to save in DB
                userAnswers.Add(new UserAnswer
                {
                    QuestionId = userAnswerDto.QuestionId,
                    SelectedOptionId = userAnswerDto.SelectedOptionId,
                    ResultId = 0  // This will be updated after saving the result
                });
            }

            // Calculate percentage based on total number of questions in the exam
            var percentage = ((double)obtainedMarks / totalQuestions) * 100;
            var passed = percentage >= 50;  // Assuming 50% is the passing mark

            // Get the latest attempt number for the user and exam
            var latestAttemptNumber = await _examResultRepository.GetLatestAttemptNumberAsync(submitResultDto.UserId, submitResultDto.ExamId);

            // Create ExamResult entity with required fields
            var examResult = new ExamResult
            {
                UserId = submitResultDto.UserId,
                ExamId = submitResultDto.ExamId,
                AttemptNumber = latestAttemptNumber + 1,  // Increment attempt number
                TotalScore = obtainedMarks,
                Percentage = percentage,
                Passed = passed,
                CompletedDate = DateTime.UtcNow,
                Duration = submitResultDto.Duration,
                markforreview = submitResultDto.markforreview
            };

            // Save the ExamResult
            var savedResult = await _examResultRepository.SubmitExamResultAsync(examResult);

            // Update the ResultId in UserAnswers and save them
            userAnswers.ForEach(ua => ua.ResultId = savedResult.ExamResultId);
            await _examResultRepository.AddUserAnswersAsync(userAnswers);

            // Return the saved result data
            var examResultDto = _mapper.Map<SubmitExamResultDto>(savedResult);
            return Ok(examResultDto);
        }*/