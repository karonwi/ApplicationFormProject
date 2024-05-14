using ApplicationForm.API.DTOs;
using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationForm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationFormsController : ControllerBase
    {
        private readonly IApplicationFormService _applicationFormService;
        private readonly IMapper _mapper;

        public ApplicationFormsController(IApplicationFormService applicationFormService, IMapper mapper)
        {
            _applicationFormService = applicationFormService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplicationForm([FromBody] ApplicationFormDto applicationFormDto)
        {
            var applicationForm = _mapper.Map<ApplicationFormModel>(applicationFormDto);
            await _applicationFormService.CreateApplicationFormAsync(applicationForm);
            var resultDto = _mapper.Map<ApplicationFormDto>(applicationForm);
            return CreatedAtAction(nameof(GetApplicationForm), new { id = applicationForm.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplicationForm(Guid id, [FromBody] ApplicationFormDto applicationFormDto)
        {
            if (id != applicationFormDto.Id)
                return BadRequest("ID mismatch in the URL and body.");

            var applicationForm = _mapper.Map<ApplicationFormModel>(applicationFormDto);
            await _applicationFormService.UpdateApplicationFormAsync(applicationForm);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationForms()
        {
            var applicationForms = await _applicationFormService.GetAllApplicationFormsAsync();
            var applicationFormDtos = _mapper.Map<IEnumerable<ApplicationFormDto>>(applicationForms);
            return Ok(applicationFormDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationForm(Guid id)
        {
            var applicationForm = await _applicationFormService.GetApplicationFormByIdAsync(id);
            if (applicationForm == null)
                return NotFound();

            var applicationFormDto = _mapper.Map<ApplicationFormDto>(applicationForm);
            return Ok(applicationFormDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationForm(Guid id)
        {
            await _applicationFormService.DeleteApplicationFormAsync(id);
            return NoContent();
        }
    }
}
