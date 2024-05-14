using ApplicationForm.API.DTOs;
using ApplicationForm.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForm.API.Validation
{
    public class QuestionDtoValidator : AbstractValidator<QuestionDto>
    {
        public QuestionDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleFor(x => x.MaxLength)
                .GreaterThan(0).When(x => x.type == (int)QuestionType.Paragraph)
                .WithMessage("Paragraph type questions must specify a valid MaxLength.");

            RuleFor(x => x.Choices)
                .NotNull().When(x => x.type == (int)QuestionType.Dropdown || x.type == (int)QuestionType.MultipleChoice)
                .WithMessage("Choices are required for Dropdown and Multiple Choice questions.")
                .Must(choices => choices.Count >= 2).When(x => x.type == (int)QuestionType.Dropdown || x.type == (int)QuestionType.MultipleChoice)
                .WithMessage("Choices must include at least two items.");

            RuleFor(x => x.DefaultAnswer)
                .NotNull().When(x => x.type == (int)QuestionType.YesNo)
                .WithMessage("Default answer must be provided for Yes/No questions.");
        }
    }
}
