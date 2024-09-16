namespace Auth.Application.DTOs;

public class CommentDTO
{
    public int CommentId { get; set; }
    public string Content { get; set; }
    public int ArticleId { get; set; } // Referencing article ID
    public int UserId { get; set; }    // Referencing user ID
    public DateTime CreatedAt { get; set; }
}