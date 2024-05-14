using ApplicationForm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Domain.Interfaces
{
    public interface IApplicationFormService
    {
        Task<ApplicationFormModel> CreateApplicationFormAsync(ApplicationFormModel applicationForm);
        Task<ApplicationFormModel> UpdateApplicationFormAsync(ApplicationFormModel applicationForm);
        Task DeleteApplicationFormAsync(Guid id);
        Task<ApplicationFormModel> GetApplicationFormByIdAsync(Guid id);
        Task<IEnumerable<ApplicationFormModel>> GetAllApplicationFormsAsync();
    }
}
