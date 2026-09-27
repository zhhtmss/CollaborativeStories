using CollaborativeStories.Features.Rounds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativeStories.Controllers;

[ApiController]
[Route("api/rounds")]
public class RoundsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoundsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("branch/{branchId:guid}")]
    public async Task<IActionResult> Get(
        Guid branchId)
    {
        var result =
            await _mediator.Send(
                new GetRoundsQuery(branchId));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateRoundCommand command)
    {
        try
        {
            var id =
                await _mediator.Send(command);

            return Ok(new
            {
                message = "Раунд створено",
                roundId = id
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