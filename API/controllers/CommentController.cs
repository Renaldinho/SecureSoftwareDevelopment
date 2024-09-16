using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    // GET: api/Comments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        return Ok(await _commentService.GetAllCommentsAsync());
    }

    // GET: api/Comments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment);
    }

    // POST: api/Comments
    [HttpPost]
    public async Task<ActionResult<Comment>> PostComment([FromBody] Comment comment)
    {
        await _commentService.AddCommentAsync(comment);
        return CreatedAtAction(nameof(GetComment), new { id = comment.CommentId }, comment);
    }

    // PUT: api/Comments/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutComment(int id, [FromBody] Comment comment)
    {
        if (id != comment.CommentId)
        {
            return BadRequest();
        }

        await _commentService.UpdateCommentAsync(comment);
        return NoContent();
    }

    // DELETE: api/Comments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }
}