using social_api.Migrations;
using social_api.Models;

namespace social_api.Repositories;

public class PostRepository: IPostRepository
{
    private readonly SocialDbContext _context;

    public PostRepository(SocialDbContext context)
    {
        _context = context;
    }

    public Post CreatePost(Post newPost)
    {
        _context.Post.Add(newPost);
        _context.SaveChanges();
        return newPost;
    }

    public void DeletePostById(int postId)
    {
        var post = _context.Post.Find(postId);
        if (post != null) {
            _context.Post.Remove(post);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Post> GetAllPosts()
    {
        return _context.Post.ToList();
    }

    public Post? GetPostById(int postId)
    {
        return _context.Post.SingleOrDefault(p => p.PostId == postId);
    }

    public IEnumerable<Post> GetPostsByUserId(int userId)
    {
        var postList = _context.Post.Where(p => p.OwnerId == userId).ToList();
        return postList;
    }

    public Post UpdatePost(Post newPost)
    {
        var originalPost = _context.Post.Find(newPost.PostId);
        if (originalPost != null) {
            originalPost.PostText = newPost.PostText;
            _context.SaveChanges();
        }
        return originalPost;
    }
}