using EnglishVocab.Application.Features.UserProgress.Commands;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserProgressSummary;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetProgressByWord;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetRecentlyStudied;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetDueForReview;
using EnglishVocab.Application.Features.StudyManagement.Progress.Queries.GetUserStatistics;
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
    public class UserProgressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserProgressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<UserProgressSummaryDto>> GetUserProgressSummary([FromQuery] string userId)
        {
            var query = new GetUserProgressSummaryQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("by-word")]
        public async Task<ActionResult<WordProgressDto[]>> GetProgressByWord([FromQuery] string userId)
        {
            var query = new GetProgressByWordQuery { UserId = userId };
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpGet("recently-studied")]
        public async Task<ActionResult<WordProgressDto[]>> GetRecentlyStudied([FromQuery] string userId, [FromQuery] int count = 10)
        {
            var query = new GetRecentlyStudiedQuery { UserId = userId, Count = count };
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpGet("due-for-review")]
        public async Task<ActionResult<WordProgressDto[]>> GetDueForReview([FromQuery] string userId, [FromQuery] int count = 10)
        {
            var query = new GetDueForReviewQuery { UserId = userId, Count = count };
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<UserStatisticsDto>> GetUserStatistics([FromQuery] string userId)
        {
            var query = new GetUserStatisticsQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("reset")]
        public async Task<ActionResult> ResetProgress([FromBody] ResetProgressCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { success = result });
        }
    }
} 
 