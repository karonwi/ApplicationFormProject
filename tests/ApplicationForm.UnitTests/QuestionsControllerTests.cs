using ApplicationForm.API.Controllers;
using ApplicationForm.API.DTOs;
using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Enums;
using ApplicationForm.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationForm.UnitTests
{
    public class QuestionsControllerTests
    {
        private readonly QuestionsController _controller;
        private readonly Mock<IQuestionService> _mockService;
        private readonly Mock<IMapper> _mockMapper;

        public QuestionsControllerTests()
        {
            _mockService = new Mock<IQuestionService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new QuestionsController(_mockService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateQuestion_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var questionDto = new QuestionDto { id = Guid.NewGuid(), type = (int)QuestionType.Paragraph, Content = "Sample question" };
            var question = new Question { id = questionDto.id, type = QuestionType.Paragraph, Content = questionDto.Content };

            _mockMapper.Setup(m => m.Map<Question>(questionDto)).Returns(question);
            _mockMapper.Setup(m => m.Map<QuestionDto>(question)).Returns(questionDto);
            _mockService.Setup(s => s.CreateQuestionAsync(question)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateQuestion(questionDto);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetQuestion), actionResult.ActionName);
        }

        [Fact]
        public async Task GetQuestion_ShouldReturnQuestionDto()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var question = new Question { id = questionId };
            var questionDto = new QuestionDto { id = questionId };

            _mockService.Setup(s => s.GetQuestionByIdAsync(questionId)).ReturnsAsync(question);
            _mockMapper.Setup(m => m.Map<QuestionDto>(question)).Returns(questionDto);

            // Act
            var result = await _controller.GetQuestion(questionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(questionDto, okResult.Value);
        }

        [Fact]
        public async Task GetQuestions_ShouldReturnListOfQuestionDtos()
        {
            // Arrange
            var questions = new List<Question> { new Question { id = Guid.NewGuid() } };
            var questionDtos = new List<QuestionDto> { new QuestionDto { id = Guid.NewGuid() } };

            _mockService.Setup(s => s.GetAllQuestionsAsync()).ReturnsAsync(questions);
            _mockMapper.Setup(m => m.Map<IEnumerable<QuestionDto>>(questions)).Returns(questionDtos);

            // Act
            var result = await _controller.GetQuestions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(questionDtos, okResult.Value);
        }

        [Fact]
        public async Task DeleteQuestion_ShouldReturnNoContent()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var questionType = QuestionType.Paragraph;

            // Act
            var result = await _controller.DeleteQuestion(questionId, questionType);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.DeleteQuestionAsync(questionId, (QuestionType)questionType), Times.Once);
        }
    }
}
