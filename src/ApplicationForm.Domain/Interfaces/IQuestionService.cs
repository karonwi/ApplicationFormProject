using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Domain.Interfaces
{
    public interface IQuestionService
    {
        Task CreateQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task<Question> GetQuestionByIdAsync(Guid id);
        Task DeleteQuestionAsync(Guid id, QuestionType type);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
    }
}
