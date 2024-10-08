﻿namespace Core.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] Salt { get; set; }
    public UserRole Role { get; set; } = UserRole.Guest;
    
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

public enum UserRole
{
    Guest,
    Subscriber,
    Writer,
    Editor
}