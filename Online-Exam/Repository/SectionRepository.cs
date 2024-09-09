using Microsoft.EntityFrameworkCore;
using Online_Exam.Data;
using Online_Exam.Models;
using Online_Exam.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Exam.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly Online_ExamContext _context;

        public SectionRepository(Online_ExamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Section>> GetAllSectionsAsync()
        {
            return await _context.Sections
                .Include(s => s.Questions)
                .ToListAsync();
        }

        public async Task<Section> GetSectionByIdAsync(int sectionId)
        {
            return await _context.Sections
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(s => s.SectionId == sectionId);
        }

        public async Task<IEnumerable<Section>> GetSectionsByExamIdAsync(int examId)
        {
            return await _context.Sections
                .Where(s => s.ExamId == examId)
                .Include(s => s.Questions)
                .ToListAsync();
        }

        public async Task CreateSectionAsync(Section section)
        {
            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSectionAsync(Section section)
        {
            _context.Sections.Update(section);
            await _context.SaveChangesAsync();
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
    }
}
