using CollaborativeStories.Features.MergeRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativeStories.Controllers;

[ApiController]
[Route("api/merge")]
public class MergeRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MergeRequestsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result =
            await _mediator.Send(
                new GetMergeRequestsQuery());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateMergeRequestCommand command)
    {
        try
        {
            var id =
                await _mediator.Send(command);

            return Ok(new
            {
                message = "Merge request створено",
                mergeRequestId = id
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

    [HttpPost("vote")]
    public async Task<IActionResult> Vote(
        VoteMergeRequestCommand command)
    {
        try
        {
            await _mediator.Send(command);

            return Ok(new
            {
                message = "Голос за merge прийнято"
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