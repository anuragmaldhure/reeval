using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Exam.DTOs.Online_Exam.DTOs;
using Online_Exam.Models;
using Online_Exam.Repositories.Interfaces;

namespace Online_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionRepository questionRepository, IOptionRepository optionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
            _mapper = mapper;
        }

        // GET: api/Question
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _questionRepository.GetAllQuestionsAsync();
            var questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questions);
            return Ok(questionDtos);
        }

        // GET: api/Question/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound();

            var questionDto = _mapper.Map<QuestionDto>(question);
            return Ok(questionDto);
        }

        // POST: api/Question
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the Question and Options together in one go
            var question = _mapper.Map<Question>(createQuestionDto);

            // Add the Question with its related Options in one transaction
            await _questionRepository.CreateQuestionAsync(question);

            // No need to add options separately, they are part of the Question entity now

            var questionDto = _mapper.Map<QuestionDto>(question);
            return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionId }, questionDto);
        }



        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound();

            await _questionRepository.DeleteQuestionAsync(id);
            return NoContent();
        }
    }

}
