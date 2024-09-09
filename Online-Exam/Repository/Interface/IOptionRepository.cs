using Online_Exam.Models;
using System.Threading.Tasks;

namespace Online_Exam.Repositories.Interfaces
{
    public interface IOptionRepository
    {
        Task CreateOptionAsync(Option option);
        Task UpdateOptionAsync(Option option);
        Task DeleteOptionAsync(int optionId);
    }
}
