using MediatR;
using Microsoft.AspNetCore.Mvc;
using n5.Application.Commands;
using n5.Application.DTOs;
using n5.Application.Queries;

namespace n5app.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly ILogger<PermissionsController> _logger;
        private readonly IMediator _mediator;

        public PermissionsController(IMediator mediator, ILogger<PermissionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetPermissions(bool SearchTerm)
        {
            var permissions = await _mediator.Send(new GetPermissionsQuery(SearchTerm));
            _logger.LogInformation("Method Get Permission - Ok");
            return Ok(permissions);
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDto>> Create(CreatePermissionCommand command)
        {
            var permissionItem = await _mediator.Send(command);
            _logger.LogInformation("Method Create Permission - Created permission Ok");
            return CreatedAtAction(nameof(CreatePermissionCommand), new { id = permissionItem.Id }, permissionItem);

        }

        [HttpPut]
        public async Task<IActionResult> Update(int Id, UpdatePermissionCommand command)
        {
            if (Id != command.Id)
            {
                _logger.LogInformation("Method Update Permission - Bad Request");
                return BadRequest();
            }

            var permissionItem = await _mediator.Send(command);
            if (permissionItem !=null)
            {
                _logger.LogInformation("Method Update Permission - Not Found");
                return NotFound();
                
            }

            return NoContent();
        }
    }

}
