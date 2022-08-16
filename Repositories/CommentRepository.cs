
using social_api.Migrations;
using social_api.Models;

namespace social_api.Repositories;
public class CommentRepository: ICommentRepository
{
    private readonly SocialDbContext _context;
    
    public CommentRepository(SocialDbContext context)
    {
        _context = context;
    }

    public Comment CreateComment(Comment newComment)
    {
        _context.Comment.Add(newComment);
        _context.SaveChanges();
        return newComment;
    }

    public void DeleteCommentById(int commentId)
    {
        var comment = _context.Comment.Find(commentId);
        if (comment != null) {
            _context.Comment.Remove(comment);
            _context.SaveChanges();
        }
    }


    public IEnumerable<Comment> GetAllComments()
    {
        return _context.Comment.ToList();
    }

    public Comment? GetCommentById(int commentId)
    {
        return _context.Comment.SingleOrDefault(c => c.CommentId == commentId);
    }

    public Comment UpdateComment(Comment newComment)
    {
        var originalComment = _context.Comment.Find(newComment.CommentId);
        if (originalComment != null) {
            originalComment.CommentText = newComment.CommentText;
            _context.SaveChanges();
        }
        return originalComment;
    }
}