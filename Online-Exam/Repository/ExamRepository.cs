using Online_Exam.Data;
using Online_Exam.Models;
using Online_Exam.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace Online_Exam.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly Online_ExamContext _context;

        public ExamRepository(Online_ExamContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Exam>> GetAllExamsAsync()
        {
            return await _context.Exams
                                 .Include(e => e.Sections)
                                 .ToListAsync();
        }

        public async Task<Exam> GetExamByIdAsync(int examId)
        {
            return await _context.Exams
                                 .Include(e => e.Sections)
                                 .FirstOrDefaultAsync(e => e.ExamId == examId);
        }

        public async Task CreateExamAsync(Exam exam)
        {
            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExamAsync(Exam exam)
        {
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExamAsync(int examId)
        {
            var exam = await _context.Exams.FindAsync(examId);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
                await _context.SaveChangesAsync();
            }
        }
    }

}
