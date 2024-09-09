using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Exam.DTOs;
using Online_Exam.Repositories.Interfaces;
using Online_Exam.Repository.Interface;

namespace Online_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionActionController : ControllerBase
    {
        private readonly ISectionActionRepository _sectionActionRepository;
        private readonly IMapper _mapper;

        public SectionActionController(ISectionActionRepository sectionActionRepository, IMapper mapper)
        {
            _sectionActionRepository = sectionActionRepository;
            _mapper = mapper;
        }

        // Get sections by ExamId
        [HttpGet("exam/{examId}")]
        public async Task<IActionResult> GetSectionsByExamId(int examId)
        {
            var sections = await _sectionActionRepository.GetSectionsByExamIdAsync(examId);
            return Ok(sections);
        }

        // Get section by SectionId
        [HttpGet("{sectionId}")]
        public async Task<IActionResult> GetSectionById(int sectionId)
        {
            var section = await _sectionActionRepository.GetSectionByIdAsync(sectionId);
            if (section == null)
            {
                return NotFound();
            }
            return Ok(section);
        }

        // Create a new section
        [HttpPost]
        public async Task<IActionResult> CreateSection([FromBody] SectionPostDTO sectionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _sectionActionRepository.CreateSectionAsync(sectionDto);

            return Ok();
        }

        // Update a section
        [HttpPut("{sectionId}")]
        public async Task<IActionResult> UpdateSection(int sectionId, [FromBody] SectionPostDTO sectionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _sectionActionRepository.UpdateSectionAsync(sectionId, sectionDto);

            return NoContent();
        }

        // Delete a section
        [HttpDelete("{sectionId}")]
        public async Task<IActionResult> DeleteSection(int sectionId)
        {
            await _sectionActionRepository.DeleteSectionAsync(sectionId);
            return NoContent();
        }

        // Add a new question to a section
        [HttpPost("{sectionId}/questions")]
        public async Task<IActionResult> AddQuestion(int sectionId, [FromBody] QuestionPostDTO questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _sectionActionRepository.AddQuestionAsync(sectionId, questionDto);
            return Ok();
        }

        // Delete a question by questionId
        [HttpDelete("questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            await _sectionActionRepository.DeleteQuestionAsync(questionId);
            return NoContent();
        }
    }


}
