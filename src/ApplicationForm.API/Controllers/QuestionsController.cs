using ApplicationForm.API.DTOs;
using ApplicationForm.Domain.Entities;
using ApplicationForm.Domain.Enums;
using ApplicationForm.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationForm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionsController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto)
        {
            try
            {
                questionDto.id = Guid.NewGuid();
                var question = _mapper.Map<Question>(questionDto);
                await _questionService.CreateQuestionAsync(question);
                var resultDto = _mapper.Map<QuestionDto>(question);
                return CreatedAtAction(nameof(GetQuestion), new { id = question.id }, resultDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Invalid question type: {questionDto.type}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] QuestionDto questionDto)
        {
            if (id != questionDto.id)
                return BadRequest("ID mismatch in the URL and body.");

            var question = _mapper.Map<Question>(questionDto);
            await _questionService.UpdateQuestionAsync(question);
            return NoContent();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _questionService.GetAllQuestionsAsync();
            var questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questions);
            return Ok(questionDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(Guid id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound();
            var questionDto = _mapper.Map<QuestionDto>(question);
            return Ok(questionDto);
        }

        [HttpGet("byType")]
        public async Task<IActionResult> GetQuestions([FromQuery] int type)
        {
            try
            {
                var questions = await _questionService.GetQuestionsByType(type);
                var questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questions);
                return Ok(questionDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id, QuestionType type)
        {
            await _questionService.DeleteQuestionAsync(id, type);
            return NoContent();
        }
    }
}
