using HKBlog.Models;

namespace HKBlog.Controllers
{
    public interface IPostController
    {
        Task<bool> AddPostToDb(Post post);
        Task<bool> DeletePost(string id, string postOption);
        Task<List<Post>> GetAllPostFromDb(string postOption);
        Task<Post> GetPostFromDb(string id, string postOption);
        Task<int> GetAllLikes(string tablename);
        void AddLikes(string Tablename, string key);
        Task<bool> DoesUserLikeOnPostExist(string Tablename, string key);
        Task<List<Comment>> GetAllComments(string postId);
        Task<bool> AddComments(Comment comment);
    }
}