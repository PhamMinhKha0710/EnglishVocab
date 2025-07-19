using EnglishVocab.Application.Common.Models;
using EnglishVocab.Application.Features.DifficultyLevels.Commands;
using EnglishVocab.Application.Features.DifficultyLevels.DTOs;
using EnglishVocab.Application.Features.DifficultyLevels.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishVocab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyLevelsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DifficultyLevelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll(
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null,
            [FromQuery] int start = 0,
            [FromQuery] int length = 0,
            [FromQuery] string orderBy = "Value",
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

            var query = new GetDifficultyLevelsQuery
            {
                UsePagination = request.IsPagingRequest(),
                PaginationParams = request
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DifficultyLevelDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetDifficultyLevelByIdQuery { Id = id });
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DifficultyLevelDto>> Create([FromBody] CreateDifficultyLevelCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DifficultyLevelDto>> Update(int id, [FromBody] UpdateDifficultyLevelCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");
                
            var result = await _mediator.Send(command);
            
            if (result == null)
                return NotFound();
                
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var command = new DeleteDifficultyLevelCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound();
                
            return Ok(true);
        }
    }
} 