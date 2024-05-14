using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Domain.Interfaces
{
    public interface IQuestionRepository
    {
        Task<Question> AddQuestionAsync(Question question);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(Guid id);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(Guid id, QuestionType type);
    }
}
