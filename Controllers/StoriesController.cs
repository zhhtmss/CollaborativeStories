using CollaborativeStories.Data;
using CollaborativeStories.Features.Stories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Controllers;

[ApiController]
[Route("api/stories")]
public class StoriesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMediator _mediator;

    public StoriesController(
        AppDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stories = await _context.Stories
            .AsNoTracking()
            .ToListAsync();

        return Ok(stories);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var story = await _context.Stories
            .AsNoTracking()
            .Include(s => s.Branches)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (story == null)
            return NotFound();

        return Ok(story);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateStoryCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new { id });
    }
}