using ApplicationForm.Application.Services;
using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Enums;
using ApplicationForm.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationForm.UnitTests
{
    public class QuestionServiceTests
    {
        private readonly QuestionService _questionService;
        private readonly Mock<IQuestionRepository> _mockRepository;

        public QuestionServiceTests()
        {
            _mockRepository = new Mock<IQuestionRepository>();
            _questionService = new QuestionService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateQuestionAsync_ShouldCallRepositoryAddMethod()
        {
            // Arrange
            var question = new Question { id = Guid.NewGuid(), type = QuestionType.Paragraph, Content = "Sample question", MaxLength = 500 };

            // Act
            await _questionService.CreateQuestionAsync(question);

            // Assert
            _mockRepository.Verify(repo => repo.AddQuestionAsync(question), Times.Once);
        }

        [Fact]
        public async Task GetQuestionByIdAsync_ShouldReturnQuestion()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var question = new Question { id = questionId };
            _mockRepository.Setup(repo => repo.GetQuestionByIdAsync(questionId))
                .ReturnsAsync(question);

            // Act
            var result = await _questionService.GetQuestionByIdAsync(questionId);

            // Assert
            Assert.Equal(question, result);
        }

        [Fact]
        public async Task GetAllQuestionsAsync_ShouldReturnListOfQuestions()
        {
            // Arrange
            var questions = new List<Question> { new Question { id = Guid.NewGuid() } };
            _mockRepository.Setup(repo => repo.GetAllQuestionsAsync())
                .ReturnsAsync(questions);

            // Act
            var result = await _questionService.GetAllQuestionsAsync();

            // Assert
            Assert.Equal(questions, result);
        }

        [Fact]
        public async Task DeleteQuestionAsync_ShouldCallRepositoryDeleteMethod()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var questionType = QuestionType.Paragraph;

            // Act
            await _questionService.DeleteQuestionAsync(questionId, questionType);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteQuestionAsync(questionId, questionType), Times.Once);
        }
    }
}
