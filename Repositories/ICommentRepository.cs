using social_api.Models;

namespace social_api.Repositories;

public interface ICommentRepository
{
    IEnumerable<Comment> GetAllComments();
    Comment? GetCommentById(int commentId);
    Comment CreateComment(Comment commentId);
    Comment UpdateComment(Comment commentId);
    void DeleteCommentById(int commentId);
}