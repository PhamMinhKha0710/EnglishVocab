using EnglishVocab.Application.Features.StudySessions.Commands.EndSession;
using EnglishVocab.Application.Features.StudySessions.Commands.MarkFlashcard;
using EnglishVocab.Application.Features.StudySessions.Commands.StartSession;
using EnglishVocab.Application.Features.StudySessions.Queries.GetUserSessions;
using EnglishVocab.Application.Features.StudySessions.Queries.GetStudySession;
using EnglishVocab.Application.Features.StudyManagement.Sessions.Queries.GetNextFlashcard;
using EnglishVocab.Application.Features.StudyManagement.Sessions.Queries.GetSessionStatistics;
using EnglishVocab.Application.Features.StudyManagement.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnglishVocab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sessions")]
        public async Task<ActionResult<StudySessionDto>> StartSession([FromBody] StartSessionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("sessions/{sessionId}/end")]
        public async Task<ActionResult<StudySessionDto>> EndSession(int sessionId, [FromBody] EndSessionCommand command)
        {
            if (sessionId != command.SessionId)
                return BadRequest();

            var result = await _mediator.Send(command);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }

        [HttpGet("sessions/{sessionId}")]
        public async Task<ActionResult<StudySessionDto>> GetSession(int sessionId)
        {
            var query = new GetStudySessionQuery { SessionId = sessionId };
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }

        [HttpGet("sessions")]
        public async Task<ActionResult<StudySessionDto[]>> GetUserSessions([FromQuery] string userId)
        {
            var query = new GetUserSessionsQuery { UserId = userId };
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpGet("sessions/{sessionId}/statistics")]
        public async Task<ActionResult<SessionStatisticsDto>> GetSessionStatistics(int sessionId)
        {
            var query = new GetSessionStatisticsQuery { SessionId = sessionId };
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }

        [HttpGet("sessions/{sessionId}/next-flashcard")]
        public async Task<ActionResult<FlashcardDto>> GetNextFlashcard(int sessionId)
        {
            var query = new GetNextFlashcardQuery { SessionId = sessionId };
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound("No more flashcards available or session ended");
                
            return Ok(result);
        }

        [HttpPost("sessions/mark-flashcard")]
        public async Task<ActionResult<FlashcardDto>> MarkFlashcard([FromBody] MarkFlashcardCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result == null)
                return NotFound("No more flashcards available or session ended");
                
            return Ok(result);
        }
    }
} 
 