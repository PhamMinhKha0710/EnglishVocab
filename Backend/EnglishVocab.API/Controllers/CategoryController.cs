using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Categories.Commands;
using EnglishVocab.Application.Features.Categories.DTOs;
using EnglishVocab.Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnglishVocab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<DataTableResponse<CategoryDto>>> GetCategories(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "Name",
            [FromQuery] bool ascending = true)
        {
            var query = new GetPaginatedCategoriesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                Ascending = ascending
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var query = new GetCategoryByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return Ok(true);
        }
    }
} 