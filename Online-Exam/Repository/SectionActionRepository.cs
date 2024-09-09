using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online_Exam.Data;
using Online_Exam.DTOs;
using Online_Exam.Repository.Interface;
using Online_Exam.Models;


namespace Online_Exam.Repository
{
    public class SectionActionRepository : ISectionActionRepository
    {
        private readonly Online_ExamContext _context;
        private readonly IMapper _mapper;

        public SectionActionRepository(Online_ExamContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SectionGetDTO>> GetSectionsByExamIdAsync(int examId)
        {
            var sections = await _context.Sections
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .Where(s => s.ExamId == examId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SectionGetDTO>>(sections);
        }

        public async Task<SectionGetDTO> GetSectionByIdAsync(int sectionId)
        {
            var section = await _context.Sections
                .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(s => s.SectionId == sectionId);

            return _mapper.Map<SectionGetDTO>(section);
        }

        public async Task CreateSectionAsync(SectionPostDTO sectionDto)
        {
            var section = _mapper.Map<Section>(sectionDto);
            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSectionAsync(int sectionId, SectionPostDTO sectionDto)
        {
            var existingSection = await _context.Sections.FindAsync(sectionId);
            if (existingSection != null)
            {
                _mapper.Map(sectionDto, existingSection);
                _context.Sections.Update(existingSection);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSectionAsync(int sectionId)
        {
            var section = await _context.Sections.FindAsync(sectionId);
            if (section != null)
            {
                _context.Sections.Remove(section);
                await _context.SaveChangesAsync();
            }
        }
        // Add a question to a section
        public async Task AddQuestionAsync(int sectionId, QuestionPostDTO questionDto)
        {
            var section = await _context.Sections.Include(s => s.Questions).FirstOrDefaultAsync(s => s.SectionId == sectionId);
            if (section == null)
            {
                throw new KeyNotFoundException("Section not found.");
            }

            var question = _mapper.Map<Question>(questionDto);
            section.Questions.Add(question);  // Add the question to the section
            await _context.SaveChangesAsync();
        }

        // Delete a question
        public async Task DeleteQuestionAsync(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
            {
                throw new KeyNotFoundException("Question not found.");
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }


}
