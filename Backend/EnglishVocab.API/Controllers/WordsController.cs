using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishVocab.Application.Features.VocabularyManagement.Words.Commands;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using EnglishVocab.Application.Features.VocabularyManagement.Words.Queries;
using EnglishVocab.Application.Features.Words.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishVocab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Words
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<WordDto>>> GetAllWords(
            [FromQuery] string searchTerm, 
            [FromQuery] int? categoryId, 
            [FromQuery] int? difficultyLevelId)
        {
            var query = new GetAllWordsQuery
            {
                SearchTerm = searchTerm,
                CategoryId = categoryId,
                DifficultyLevelId = difficultyLevelId
            };
            
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/Words/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WordDto>> GetWord(int id)
        {
            var result = await _mediator.Send(new GetWordByIdQuery { Id = id });
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }

        // POST: api/Words
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WordDto>> CreateWord([FromBody] CreateWordCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetWord), new { id = result.Id }, result);
        }

        // PUT: api/Words/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateWord(int id, [FromBody] UpdateWordCommand command)
        {
            if (id != command.Id)
                return BadRequest();
                
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/Words/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWord(int id)
        {
            await _mediator.Send(new DeleteWordCommand { Id = id });
            return NoContent();
        }
    }
} 
 