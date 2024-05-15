
using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Enums;
using ApplicationForm.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task CreateQuestionAsync(Question question)
        {
            await _questionRepository.AddQuestionAsync(question);
        }
        public async Task UpdateQuestionAsync(Question question)
        {
            await _questionRepository.UpdateQuestionAsync(question);
        }
        public async Task DeleteQuestionAsync(Guid id, QuestionType type)
        {
            await _questionRepository.DeleteQuestionAsync(id, type);
        }
        public async Task<IEnumerable<Question>> GetQuestionsByType(int type)
        {
            return await _questionRepository.GetQuestionsByType(type);
        }

        public async Task<Question> GetQuestionByIdAsync(Guid id)
        {
            return await _questionRepository.GetQuestionByIdAsync(id);
        }
        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _questionRepository.GetAllQuestionsAsync();
        }
    }
}
