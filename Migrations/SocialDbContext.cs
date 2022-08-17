using Microsoft.EntityFrameworkCore;
using social_api.Models;

namespace social_api.Migrations;
public class SocialDbContext : DbContext
{
    public DbSet<Post> Post { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<User> Users { get; set; }

    public SocialDbContext(DbContextOptions<SocialDbContext> options): base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity => {
            entity.HasKey(e => e.PostId);
            entity.Property(e => e.PostText).IsRequired();
            entity.Property(e => e.OwnerId).IsRequired();
            entity.Property(e => e.CreatedOn).IsRequired();
            entity.Property(e => e.UpdatedOn);
            entity.Property(e => e.DeletedOn);
            entity.Property(e => e.CommentId);
        });

        modelBuilder.Entity<User>(entity => {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserName).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName);
            entity.Property(e => e.LastName);
            entity.Property(e => e.Location);
            entity.Property(e => e.Title);
        });

        modelBuilder.Entity<Comment>(entity => {
            entity.HasKey(e => e.CommentId);
            entity.Property(e => e.CommentText).IsRequired();
            entity.Property(e => e.OwnerId).IsRequired();
            entity.Property(e => e.CreatedOn).IsRequired();
            entity.Property(e => e.UpdatedOn);
            entity.Property(e => e.DeletedOn); 
        });
    }
}