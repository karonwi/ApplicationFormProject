using ApplicationForm.API.Controllers;
using ApplicationForm.API.DTOs;
using ApplicationForm.Domain.Entities;
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
    public class ApplicationFormsControllerTests
    {
        private readonly ApplicationFormsController _controller;
        private readonly Mock<IApplicationFormService> _mockService;
        private readonly Mock<IMapper> _mockMapper;

        public ApplicationFormsControllerTests()
        {
            _mockService = new Mock<IApplicationFormService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ApplicationFormsController(_mockService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateApplicationForm_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var applicationFormDto = new ApplicationFormDto { Id = Guid.NewGuid() };
            var applicationForm = new ApplicationFormModel { Id = applicationFormDto.Id };

            _mockMapper.Setup(m => m.Map<ApplicationFormModel>(applicationFormDto)).Returns(applicationForm);
            _mockMapper.Setup(m => m.Map<ApplicationFormDto>(applicationForm)).Returns(applicationFormDto);
            _mockService.Setup(s => s.CreateApplicationFormAsync(applicationForm)).ReturnsAsync(applicationForm);

            // Act
            var result = await _controller.CreateApplicationForm(applicationFormDto);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetApplicationForm), actionResult.ActionName);
        }

        [Fact]
        public async Task GetApplicationForm_ShouldReturnApplicationFormDto()
        {
            // Arrange
            var applicationFormId = Guid.NewGuid();
            var applicationForm = new ApplicationFormModel { Id = applicationFormId };
            var applicationFormDto = new ApplicationFormDto { Id = applicationFormId };

            _mockService.Setup(s => s.GetApplicationFormByIdAsync(applicationFormId)).ReturnsAsync(applicationForm);
            _mockMapper.Setup(m => m.Map<ApplicationFormDto>(applicationForm)).Returns(applicationFormDto);

            // Act
            var result = await _controller.GetApplicationForm(applicationFormId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(applicationFormDto, okResult.Value);
        }

        [Fact]
        public async Task GetApplicationForms_ShouldReturnListOfApplicationFormDtos()
        {
            // Arrange
            var applicationForms = new List<ApplicationFormModel> { new ApplicationFormModel { Id = Guid.NewGuid() } };
            var applicationFormDtos = new List<ApplicationFormDto> { new ApplicationFormDto { Id = Guid.NewGuid() } };

            _mockService.Setup(s => s.GetAllApplicationFormsAsync()).ReturnsAsync(applicationForms);
            _mockMapper.Setup(m => m.Map<IEnumerable<ApplicationFormDto>>(applicationForms)).Returns(applicationFormDtos);

            // Act
            var result = await _controller.GetApplicationForms();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(applicationFormDtos, okResult.Value);
        }

        [Fact]
        public async Task DeleteApplicationForm_ShouldReturnNoContent()
        {
            // Arrange
            var applicationFormId = Guid.NewGuid();

            // Act
            var result = await _controller.DeleteApplicationForm(applicationFormId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.DeleteApplicationFormAsync(applicationFormId), Times.Once);
        }
    }
}
