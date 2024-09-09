using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Exam.DTOs;
using Online_Exam.Models;
using Online_Exam.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public SectionController(ISectionRepository sectionRepository, IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }

        // GET: api/Section
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllSections()
        {
            var sections = await _sectionRepository.GetAllSectionsAsync();
            var sectionDtos = _mapper.Map<IEnumerable<SectionDto>>(sections);
            return Ok(sectionDtos);
        }

        // GET: api/Section/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSection(int id)
        {
            var section = await _sectionRepository.GetSectionByIdAsync(id);
            if (section == null)
                return NotFound();

            var sectionDto = _mapper.Map<SectionDto>(section);
            return Ok(sectionDto);
        }

        // GET: api/Section/Exam/5
        [HttpGet("Exam/{examId}")]
        public async Task<IActionResult> GetSectionsByExamId(int examId)
        {
            var sections = await _sectionRepository.GetSectionsByExamIdAsync(examId);
            if (sections == null || sections.Count() == 0)
                return NotFound("No sections found for the specified exam.");

            var sectionDtos = _mapper.Map<IEnumerable<SectionDto>>(sections);
            return Ok(sectionDtos);
        }

        // POST: api/Section
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateSection([FromBody] CreateSectionDto createSectionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var section = _mapper.Map<Section>(createSectionDto);
            await _sectionRepository.CreateSectionAsync(section);

            var sectionDto = _mapper.Map<SectionDto>(section);
            return CreatedAtAction(nameof(GetSection), new { id = section.SectionId }, sectionDto);
        }

        // PUT: api/Section/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> UpdateSection(int id, [FromBody] UpdateSectionDto updateSectionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var section = await _sectionRepository.GetSectionByIdAsync(id);
            if (section == null)
                return NotFound();

            _mapper.Map(updateSectionDto, section);
            await _sectionRepository.UpdateSectionAsync(section);

            return NoContent();
        }

        // DELETE: api/Section/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _sectionRepository.GetSectionByIdAsync(id);
            if (section == null)
                return NotFound();

            await _sectionRepository.DeleteSectionAsync(id);
            return NoContent();
        }
    }
}
