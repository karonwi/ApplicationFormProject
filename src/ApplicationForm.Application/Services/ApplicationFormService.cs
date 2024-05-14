using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Application.Services
{
    public class ApplicationFormService : IApplicationFormService
    {
        private readonly IApplicationFormRepository _applicationFormRepository;

        public ApplicationFormService(IApplicationFormRepository applicationFormRepository)
        {
            _applicationFormRepository = applicationFormRepository;
        }

        public async Task<ApplicationFormModel> CreateApplicationFormAsync(ApplicationFormModel applicationForm)
        {
            return await _applicationFormRepository.AddApplicationFormAsync(applicationForm);
        }

        public async Task<ApplicationFormModel> UpdateApplicationFormAsync(ApplicationFormModel applicationForm)
        {
            return await _applicationFormRepository.UpdateApplicationFormAsync(applicationForm);
        }

        public async Task DeleteApplicationFormAsync(Guid id)
        {
            await _applicationFormRepository.DeleteApplicationFormAsync(id);
        }

        public async Task<ApplicationFormModel> GetApplicationFormByIdAsync(Guid id)
        {
            return await _applicationFormRepository.GetApplicationFormByIdAsync(id);
        }

        public async Task<IEnumerable<ApplicationFormModel>> GetAllApplicationFormsAsync()
        {
            return await _applicationFormRepository.GetAllApplicationFormsAsync();
        }
    }
}
