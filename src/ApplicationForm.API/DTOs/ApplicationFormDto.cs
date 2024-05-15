using System.Text.Json.Serialization;

namespace ApplicationForm.API.DTOs
{
    public class ApplicationFormDto
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public PersonalInfoDto PersonalInfo { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
