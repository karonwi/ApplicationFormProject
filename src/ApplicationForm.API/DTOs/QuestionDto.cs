﻿namespace ApplicationForm.API.DTOs
{
    public class QuestionDto
    {
        public Guid id { get; set; }
        public int type { get; set; }
        public string? Content { get; set; }
        public bool IsRequired { get; set; }

        public int? MaxLength { get; set; } 

        public bool? DefaultAnswer { get; set; } 
        public List<string> Choices { get; set; }
        public bool? AllowMultiple { get; set; }  
        public bool? EnableOtherOption { get; set; }

        public int? MaxChoicesAllowed { get; set; }
        public QuestionDto()
        {
            Choices = new List<string>();
        }
    }
}
