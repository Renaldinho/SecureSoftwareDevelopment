using Application.Interfaces;
using Auth.Application.DTOs;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments()
    {
        var comments = await _commentService.GetAllCommentsAsync();
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDTO>> GetComment(int id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment);
    }

    [HttpPost]
    public async Task<ActionResult<CommentDTO>> PostComment([FromBody] CommentDTO commentDto)
    {
        var newComment = await _commentService.AddCommentAsync(commentDto);
        return CreatedAtAction(nameof(GetComment), new { id = newComment.CommentId }, newComment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentDTO commentDto)
    {
        if (id != commentDto.CommentId)
        {
            return BadRequest();
        }
        await _commentService.UpdateCommentAsync(commentDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }
}