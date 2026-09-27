using CollaborativeStories.Features.Branches;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativeStories.Controllers;

[ApiController]
[Route("api/branches")]
public class BranchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var result =
                await _mediator.Send(
                    new GetBranchQuery(id));

            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(new
            {
                error = ex.Message
            });
        }
    }

    [HttpPost("{id:guid}/fork")]
    public async Task<IActionResult> Fork(
        Guid id,
        ForkBranchCommand command)
    {
        var actualCommand =
            command with
            {
                BranchId = id
            };

        try
        {
            var newId =
                await _mediator.Send(actualCommand);

            return Ok(new
            {
                message = "Гілку створено",
                branchId = newId
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }
}