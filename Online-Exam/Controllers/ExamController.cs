using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Exam.DTOs;
using Online_Exam.Models;
using Online_Exam.Repository.Interface;

namespace Online_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamController(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        // GET: api/Exam
        [HttpGet]
        public async Task<IActionResult> GetAllExams()
        {
            var exams = await _examRepository.GetAllExamsAsync();
            var examsDto = _mapper.Map<IEnumerable<ExamDto>>(exams);
            return Ok(examsDto);
        }

        // GET: api/Exam/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExam(int id)
        {
            var exam = await _examRepository.GetExamByIdAsync(id);
            if (exam == null)
                return NotFound();

            var examDto = _mapper.Map<ExamDto>(exam);
            return Ok(examDto);
        }

        // POST: api/Exam
        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamDto createExamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exam = _mapper.Map<Exam>(createExamDto);
            exam.CreatedDate = DateTime.UtcNow;
            exam.IsPublished = true; // Set IsPublished to true by default

            await _examRepository.CreateExamAsync(exam);

            var examDto = _mapper.Map<ExamDto>(exam);
            return CreatedAtAction(nameof(GetExam), new { id = exam.ExamId }, examDto);
        }


        // PUT: api/Exam/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, [FromBody] UpdateExamDto updateExamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exam = await _examRepository.GetExamByIdAsync(id);
            if (exam == null)
                return NotFound();

            _mapper.Map(updateExamDto, exam);

            await _examRepository.UpdateExamAsync(exam);
            return NoContent();
        }

        // DELETE: api/Exam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var exam = await _examRepository.GetExamByIdAsync(id);
            if (exam == null)
                return NotFound();

            await _examRepository.DeleteExamAsync(id);
            return NoContent();
        }
    }
}
