using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.DTOs;
using QuizAPI.Services;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly QuestionsService _service;

        public QuestionsController(ILogger<QuestionsController> logger, QuestionsService questionsService)
        {
            _logger = logger;
            _service = questionsService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_service.Get((int)id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_service.Get());
        }
        [HttpPost]
        public async Task<IActionResult> Save(SaveQuestionDTO saveQuestionDTO)
        {
            return Ok(_service.SaveAsync(saveQuestionDTO));
        }
    }
}
