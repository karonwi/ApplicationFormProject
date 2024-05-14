using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Domain.Entities
{
    public class ApplicationFormModel
    {
        public Guid Id { get; set; } 
        public PersonalInfo PersonalInfo { get; set; }
        public List<Answer> Answers { get; set; }

        public ApplicationFormModel()
        {
            Answers = new List<Answer>();
        }
    }
}
