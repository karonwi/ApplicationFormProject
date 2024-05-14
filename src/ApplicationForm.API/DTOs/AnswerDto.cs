namespace ApplicationForm.API.DTOs
{
    public class AnswerDto
    {
        public Guid QuestionId { get; set; }
        public List<string> Answers { get; set; }
        public AnswerDto()
        {
            Answers = new List<string>(); 
        }
    }
}
