namespace ApplicationForm.API.DTOs
{
    public class ApplicationFormDto
    {
        public Guid Id { get; set; }
        public PersonalInfoDto PersonalInfo { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
