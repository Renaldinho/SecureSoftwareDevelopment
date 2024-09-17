using System.Security.Claims;
using Application.Interfaces;
using Auth.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Subscriber, Writer, Editor")]
    public async Task<ActionResult<CommentDTO>> PostComment([FromBody] CommentDTO commentDto)
    {
        var newComment = await _commentService.AddCommentAsync(commentDto);
        return CreatedAtAction(nameof(GetComment), new { id = newComment.CommentId }, newComment);
    }

    [HttpPut("{id}")]
    [Authorize]  // Ensure the user is logged in
    public async Task<IActionResult> EditComment(int id, [FromBody] CommentDTO commentDto)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        // Get the current user's ID
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Check if the current user is the author of the comment or an editor
        if (comment.UserId.ToString() == userId || User.IsInRole("Editor"))
        {
            await _commentService.UpdateCommentAsync(commentDto);
            return Ok(commentDto);
        }
        else
        {
            return Forbid("You do not have permission to edit this comment.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize]  // Ensure the user is logged in
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _commentService.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        // Get the current user's ID
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Check if the current user is the author of the comment or an editor
        if (comment.UserId.ToString() == userId || User.IsInRole("Editor"))
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
        else
        {
            return Forbid("You do not have permission to delete this comment.");
        }
    }
}