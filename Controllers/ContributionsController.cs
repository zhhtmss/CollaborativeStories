using CollaborativeStories.Features.Contributions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativeStories.Controllers;

[ApiController]
[Route("api/contributions")]
public class ContributionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContributionsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("branch/{branchId:guid}")]
    public async Task<IActionResult> GetByBranch(
        Guid branchId)
    {
        var result =
            await _mediator.Send(
                new GetContributionsQuery(branchId));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateContributionCommand command)
    {
        try
        {
            var id =
                await _mediator.Send(command);

            return Ok(new
            {
                message = "Вклад додано",
                contributionId = id
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