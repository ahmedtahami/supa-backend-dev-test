using Microsoft.AspNetCore.Mvc;
using QuizAPI.DTOs;
using QuizAPI.Services;

namespace QuizAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly ILogger<QuizController> _logger;
        private readonly QuizService _service;

        public QuizController(ILogger<QuizController> logger, QuizService quizService)
        {
            _logger = logger;
            _service = quizService;
        }
        [HttpGet("Report")]
        public async Task<IActionResult> GetReport()
        {
            var result = await _service.GetReportAsync();
            return Ok(result);
        }
        [HttpPost("SubmitAnswer")]
        public async Task<IActionResult> SubmitAnswer(SubmitAnswerDTO submitAnswerDTO)
        {
            return Ok(_service.SubmitAnswer(submitAnswerDTO));
        }
    }
}