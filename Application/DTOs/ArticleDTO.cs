namespace Auth.Application.DTOs;

public class ArticleDTO
{
    public int ArticleId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }  // Only the ID is exposed
    public DateTime CreatedAt { get; set; }
}