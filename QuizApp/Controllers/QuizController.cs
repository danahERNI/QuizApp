using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Data.DTO;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;

namespace QuizApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public QuizController(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<CreateQuizDTO>> AddQuiz(CreateQuizDTO quizDTO)
        {
            var mapQuiz = _mapper.Map<Quiz>(quizDTO);
            var newQuiz = await _quizRepository.AddQuiz(mapQuiz);
            return Ok(_mapper.Map<CreateQuizDTO>(newQuiz));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<CreateChoiceDTO>> DeleteQuiz(int id)
        {
            var removeQuiz = await _quizRepository.DeleteQuiz(id);
            if (removeQuiz == null)
            {
                return NotFound();
            }
            return Ok(removeQuiz);
        }
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            var list = await _quizRepository.GetAllQuizzes();
            return list;
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CreateQuizDTO>> GetQuizId(int id)
        {
            var getQuiz = await _quizRepository.GetQuizId(id);
            return Ok(getQuiz);
        }
        [HttpGet("Mentor/{id}")]
        public async Task<ActionResult<ICollection<TeacherQuizDTO>>> GetQuizByMentorId(int id)
        {
            var getQuiz = await _quizRepository.GetQuizByMentorId(id);
            return Ok(getQuiz);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<CreateQuizDTO>> UpdateQuiz(int id, CreateQuizDTO quizDTO)
        {
            var updated = _mapper.Map<Quiz>(quizDTO);
            updated.Id = id;
            var changeQuiz = await _quizRepository.UpdateQuiz(id, updated);
            return Ok(changeQuiz);

        }
        //[HttpPost("record-quiz")]
        //[Authorize(Roles = "Student")]

    }
}

