using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }
        public QuestionType Type { get; set; }
        public string Content { get; set; }
        public bool IsRequired { get; set; }

        public int? MaxLength { get; set; } 

        public bool? DefaultAnswer { get; set; } 

        public List<string> Choices { get; set; }
        public bool? AllowMultiple { get; set; }  
        public bool? EnableOtherOption { get; set; } 
        public int? MaxChoicesAllowed { get; set; } 

        public Question()
        {
            Choices = new List<string>(); 
        }
    }
}
