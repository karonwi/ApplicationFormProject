using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.Application.Validation
{
    public static class ValidateQuestion
    {
        public static void ValidateQuestionOnCreate(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));
            switch (question.type)
            {
                case QuestionType.Paragraph:
                    if (!question.MaxLength.HasValue || question.MaxLength <= 0)
                        throw new ArgumentException("Paragraph type questions must specify a valid MaxLength.");
                    break;
                case QuestionType.MultipleChoice:
                    if (question.Choices == null || question.Choices.Count < 2)
                        throw new ArgumentException("Multiple Choice questions must include at least two choices.");
                    if (question.MaxChoicesAllowed.HasValue && question.MaxChoicesAllowed < 1)
                        throw new ArgumentException("MaxChoicesAllowed must be at least 1.");
                    break;
                case QuestionType.Dropdown:
                    if (question.Choices == null || question.Choices.Count < 2)
                        throw new ArgumentException("Dropdown questions must include at least two choices.");
                    break;
                case QuestionType.YesNo:
                    if (!question.DefaultAnswer.HasValue)
                        throw new ArgumentException("Default answer must be provided for Yes/No questions.");
                    break;

            }
        }
    }
}
