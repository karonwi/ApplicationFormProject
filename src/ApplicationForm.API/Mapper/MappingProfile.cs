using ApplicationForm.API.DTOs;
using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Enums;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationForm.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Question, QuestionDto>()
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => (int)src.type))
            .ReverseMap()
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => (QuestionType)src.type));
            CreateMap<PersonalInfo, PersonalInfoDto>().ReverseMap();
            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<ApplicationFormModel, ApplicationFormDto>().ReverseMap();
        }
    }
}
