using CollaborativeStories.Features.Votes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativeStories.Controllers;

[ApiController]
[Route("api/votes")]
public class VotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Vote(
        VoteContributionCommand command)
    {
        try
        {
            await _mediator.Send(command);

            return Ok(new
            {
                message = "Голос прийнято"
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