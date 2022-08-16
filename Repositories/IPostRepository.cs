using social_api.Models;

namespace social_api.Repositories;

public interface IPostRepository
{
    IEnumerable<Post> GetAllPosts();
    Post? GetPostById(int postId);
    Post CreatePost(Post newPost);
    Post UpdatePost(Post newPost);
    void DeletePostById(int postId);
}