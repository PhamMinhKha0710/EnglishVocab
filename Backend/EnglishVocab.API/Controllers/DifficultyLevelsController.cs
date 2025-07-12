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
        public async Task<ActionResult<IEnumerable<DifficultyLevelDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetDifficultyLevelsQuery());
            return Ok(result);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<DataTableResponse<DifficultyLevelDto>>> GetPaginated([FromQuery] GetPaginatedDifficultyLevelsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("datatable")]
        public async Task<ActionResult<DataTableResponse<DifficultyLevelDto>>> GetDataTable([FromBody] GetDataTableDifficultyLevelsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DifficultyLevelDto>> Create([FromBody] CreateDifficultyLevelCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DifficultyLevelDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetDifficultyLevelByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DifficultyLevelDto>> Update(int id, [FromBody] UpdateDifficultyLevelCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteDifficultyLevelCommand { Id = id });
            return NoContent();
        }
    }
} 