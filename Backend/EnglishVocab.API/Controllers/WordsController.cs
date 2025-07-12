using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Categories.DTOs;
using EnglishVocab.Application.Features.Categories.Queries;
using EnglishVocab.Application.Features.DifficultyLevels.Queries;
using EnglishVocab.Application.Features.VocabularyManagement.Words.Commands;
using EnglishVocab.Application.Features.VocabularyManagement.Words.DTOs;
using EnglishVocab.Application.Features.VocabularyManagement.Words.Queries;
using EnglishVocab.Application.Features.Words.Commands;
using EnglishVocab.Constants.Constant;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllWordsQuery());
            return Ok(result);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<DataTableResponse<WordDto>>> GetPaginated([FromQuery] GetPaginatedWordsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách từ vựng theo danh mục và độ khó (tùy chọn)
        /// </summary>
        /// <param name="categoryId">ID của danh mục</param>
        /// <param name="difficultyLevel">Độ khó (tùy chọn). Giá trị hợp lệ: Basic, Intermediate, Advanced</param>
        /// <returns>Danh sách từ vựng</returns>
        [HttpGet("category/{categoryId}/difficulty/{difficultyLevel?}")]
        public async Task<ActionResult<IEnumerable<WordDto>>> GetByCategoryAndDifficulty(int categoryId, string difficultyLevel = null)
        {
            try
            {
                // Nếu có difficultyLevel, kiểm tra tính hợp lệ
                if (!string.IsNullOrEmpty(difficultyLevel) && !Enum.TryParse<DifficultyLevelType>(difficultyLevel, out _))
                {
                    return BadRequest($"Invalid difficulty level. Valid values are: {string.Join(", ", Enum.GetNames(typeof(DifficultyLevelType)))}");
                }
                
                if (string.IsNullOrEmpty(difficultyLevel))
                {
                    // Nếu không có difficultyLevel, lấy tất cả từ vựng theo danh mục
                    var query = new GetWordsByCategoryIdQuery { CategoryId = categoryId };
                    var results = await _mediator.Send(query);
                    return Ok(results);
                }
                else
                {
                    // Nếu có difficultyLevel, lọc theo cả danh mục và độ khó
                    var result = await _mediator.Send(new GetWordsByCategoryIdAndDifficultyLevelQuery 
                    { 
                        CategoryId = categoryId, 
                        DifficultyLevel = difficultyLevel 
                    });
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("datatable")]
        public async Task<ActionResult<DataTableResponse<WordDto>>> GetDataTable([FromBody] GetDataTableWordsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<WordDto>> Create([FromBody] CreateWordCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetWordByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<WordDto>> Update(int id, [FromBody] UpdateWordCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteWordCommand { Id = id });
            return NoContent();
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult<WordDto[]>> SearchWords([FromQuery] string term)
        {
            var query = new SearchWordsQuery { SearchTerm = term };
            var results = await _mediator.Send(query);
            return Ok(results);
        }
    }
} 
 