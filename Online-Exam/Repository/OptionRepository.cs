using Microsoft.EntityFrameworkCore;
using Online_Exam.Data;
using Online_Exam.Models;
using Online_Exam.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Online_Exam.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly Online_ExamContext _context;

        public OptionRepository(Online_ExamContext context)
        {
            _context = context;
        }

        public async Task CreateOptionAsync(Option option)
        {
            await _context.Options.AddAsync(option);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOptionAsync(Option option)
        {
            _context.Options.Update(option);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOptionAsync(int optionId)
        {
            var option = await _context.Options.FindAsync(optionId);
            if (option != null)
            {
                _context.Options.Remove(option);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId)
        {
            return await _context.Questions
                .Include(q => q.Options)  // Include related options
                .Where(q => q.Section.ExamId == examId)  // Filter by ExamId
                .ToListAsync();
        }
    }
}
