using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.Categories.Commands;
using EnglishVocab.Application.Features.Categories.DTOs;
using EnglishVocab.Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<ActionResult<object>> GetCategories(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null,
            [FromQuery] int start = 0,
            [FromQuery] int length = 0,
            [FromQuery] string orderBy = "Name",
            [FromQuery] string order = "asc",
            [FromQuery] string search = null)
        {
            var request = new DataTableRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                    Start = start,
                    Length = length,
                    OrderBy = orderBy,
                    Order = order,
                    Search = search
            };
            
            // Đảm bảo Start và Length được tính toán đúng từ PageNumber và PageSize
            request.NormalizeRequest();

            var query = new GetCategoriesQuery
            {
                UsePagination = request.IsPagingRequest(),
                PaginationParams = request
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