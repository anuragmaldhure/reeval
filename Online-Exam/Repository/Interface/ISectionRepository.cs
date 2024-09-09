using Online_Exam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Exam.Repositories.Interfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<Section>> GetAllSectionsAsync();
        Task<Section> GetSectionByIdAsync(int sectionId);
        Task<IEnumerable<Section>> GetSectionsByExamIdAsync(int examId); // Existing method to get sections by ExamId
        Task CreateSectionAsync(Section section);
        Task UpdateSectionAsync(Section section);
        Task DeleteSectionAsync(int sectionId);
    }
}
