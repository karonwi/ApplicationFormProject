using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationForm.Domain.Entities
{
    public class ApplicationFormModel
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public List<Answer> Answers { get; set; }

        public ApplicationFormModel()
        {
            Answers = new List<Answer>();
        }
    }
}
