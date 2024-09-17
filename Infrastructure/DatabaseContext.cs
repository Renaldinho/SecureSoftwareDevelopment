using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db", b => b.MigrationsAssembly("API"));
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; } // If you also manage users within the same context
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring the one-to-many relationship between User and Article
        modelBuilder.Entity<User>()
            .HasMany(u => u.Articles)
            .WithOne(a => a.Author)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.Cascade); // If a User is deleted, delete their articles

        // Configuring the one-to-many relationship between Article and Comment
        modelBuilder.Entity<Article>()
            .HasMany(a => a.Comments)
            .WithOne(c => c.Article)
            .HasForeignKey(c => c.ArticleId)
            .OnDelete(DeleteBehavior.Cascade); // If an Article is deleted, delete its comments

        // Configuring the one-to-many relationship between User and Comment
        modelBuilder.Entity<User>()
            .HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade); // If a User is deleted, delete their comments
    }
}