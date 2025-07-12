using EnglishVocab.Application.Features.WordSets.Commands;
using EnglishVocab.Application.Features.WordSets.Queries;
using EnglishVocab.Application.Features.VocabularyManagement.WordSets.Queries.GetAllWordSets;
using EnglishVocab.Application.Features.WordSets.Queries.GetWordSetById;
using EnglishVocab.Application.Features.WordSets.Queries.GetDefaultWordSets;
using EnglishVocab.Application.Features.WordSets.Queries.GetUserWordSets;
using EnglishVocab.Application.Features.VocabularyManagement.WordSets.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace EnglishVocab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WordSetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WordSetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<WordSetDto[]>> GetAllWordSets()
        {
            var query = new GetAllWordSetsQuery();
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpGet("default")]
        public async Task<ActionResult<WordSetDto[]>> GetDefaultWordSets()
        {
            var query = new GetDefaultWordSetsQuery();
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<WordSetDto[]>> GetUserWordSets(string userId)
        {
            var query = new GetUserWordSetsQuery { UserId = userId };
            var results = await _mediator.Send(query);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordSetDetailDto>> GetWordSetById(int id)
        {
            var query = new GetWordSetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<WordSetDto>> CreateWordSet([FromBody] CreateWordSetCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetWordSetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WordSetDto>> UpdateWordSet(int id, [FromBody] UpdateWordSetCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await _mediator.Send(command);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWordSet(int id)
        {
            var command = new DeleteWordSetCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound();
                
            return NoContent();
        }

        /// <summary>
        /// Thêm một hoặc nhiều từ vào bộ từ vựng
        /// </summary>
        /// <param name="wordSetId">ID của bộ từ vựng</param>
        /// <param name="wordIds">Danh sách ID của các từ cần thêm</param>
        /// <returns>Kết quả thêm từ</returns>
        [HttpPost("{wordSetId}/words")]
        public async Task<ActionResult> AddWordsToSet(int wordSetId, [FromBody] List<int> wordIds)
        {
            if (wordIds == null || wordIds.Count == 0)
            {
                return BadRequest("Danh sách từ không được để trống");
            }

            var successCount = 0;
            var failedWords = new List<int>();

            foreach (var wordId in wordIds)
            {
                var command = new AddWordToSetCommand 
                { 
                    WordSetId = wordSetId, 
                    WordId = wordId 
                };
                
                var result = await _mediator.Send(command);
                
                if (result)
                {
                    successCount++;
                }
                else
                {
                    failedWords.Add(wordId);
                }
            }

            return Ok(new { 
                added = successCount, 
                total = wordIds.Count,
                failedWords = failedWords
            });
        }

        /// <summary>
        /// Thêm một từ vào bộ từ vựng
        /// </summary>
        /// <param name="wordSetId">ID của bộ từ vựng</param>
        /// <param name="wordId">ID của từ cần thêm</param>
        /// <returns>Kết quả thêm từ</returns>
        [HttpPost("{wordSetId}/words/{wordId}")]
        public async Task<ActionResult> AddWordToSet(int wordSetId, int wordId)
        {
            // Sử dụng phương thức AddWordsToSet với một từ duy nhất
            return await AddWordsToSet(wordSetId, new List<int> { wordId });
        }

        [HttpDelete("{wordSetId}/words/{wordId}")]
        public async Task<ActionResult> RemoveWordFromSet(int wordSetId, int wordId)
        {
            var command = new RemoveWordFromSetCommand 
            { 
                WordSetId = wordSetId, 
                WordId = wordId 
            };
            
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound("Word set or word not found");
                
            return Ok(new { removed = true });
        }
    }
} 
 