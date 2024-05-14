using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Domain.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; } 
        public List<string> Answers { get; set; } 

        public Answer()
        {
            Answers = new List<string>(); 
        }
    }
}
