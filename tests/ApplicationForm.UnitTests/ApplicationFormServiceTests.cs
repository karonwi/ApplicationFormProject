using ApplicationForm.Application.Services;
using ApplicationForm.Domain.Entities;
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
    public class ApplicationFormServiceTests
    {
        private readonly ApplicationFormService _applicationFormService;
        private readonly Mock<IApplicationFormRepository> _mockRepository;

        public ApplicationFormServiceTests()
        {
            _mockRepository = new Mock<IApplicationFormRepository>();
            _applicationFormService = new ApplicationFormService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateApplicationFormAsync_ShouldReturnCreatedApplicationForm()
        {
            // Arrange
            var applicationForm = new ApplicationFormModel { id = Guid.NewGuid() };
            _mockRepository.Setup(repo => repo.AddApplicationFormAsync(applicationForm))
                .ReturnsAsync(applicationForm);

            // Act
            var result = await _applicationFormService.CreateApplicationFormAsync(applicationForm);

            // Assert
            Assert.Equal(applicationForm, result);
        }

        [Fact]
        public async Task GetApplicationFormByIdAsync_ShouldReturnApplicationForm()
        {
            // Arrange
            var applicationFormId = Guid.NewGuid();
            var applicationForm = new ApplicationFormModel { id = applicationFormId };
            _mockRepository.Setup(repo => repo.GetApplicationFormByIdAsync(applicationFormId))
                .ReturnsAsync(applicationForm);

            // Act
            var result = await _applicationFormService.GetApplicationFormByIdAsync(applicationFormId);

            // Assert
            Assert.Equal(applicationForm, result);
        }

        [Fact]
        public async Task GetAllApplicationFormsAsync_ShouldReturnListOfApplicationForms()
        {
            // Arrange
            var applicationForms = new List<ApplicationFormModel> { new ApplicationFormModel { id = Guid.NewGuid() } };
            _mockRepository.Setup(repo => repo.GetAllApplicationFormsAsync())
                .ReturnsAsync(applicationForms);

            // Act
            var result = await _applicationFormService.GetAllApplicationFormsAsync();

            // Assert
            Assert.Equal(applicationForms, result);
        }

        [Fact]
        public async Task DeleteApplicationFormAsync_ShouldCallRepositoryDeleteMethod()
        {
            // Arrange
            var applicationFormId = Guid.NewGuid();

            // Act
            await _applicationFormService.DeleteApplicationFormAsync(applicationFormId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteApplicationFormAsync(applicationFormId), Times.Once);
        }
    }
}
