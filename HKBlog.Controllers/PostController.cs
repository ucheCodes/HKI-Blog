using HKBlog.Database;
using HKBlog.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKBlog.Controllers
{
    public class PostController : IPostController
    {
        private readonly IDatabase database;
        public PostController(IDatabase _database)
        {
            database = _database;
        }
        public async Task<bool> AddPostToDb(Post post)
        {
            string key = JsonConvert.SerializeObject(post.Id);
            string value = JsonConvert.SerializeObject(post);
            bool res = false;
            if (post.PostOption == "blog")
            {
                res = await database.Create("Blogs", key, value);
            }
            else if (post.PostOption == "course")
            {
                res = await database.Create("Courses", key, value);
            }
            return res;
        }
        public async Task<Post> GetPostFromDb(string id, string postOption)
        {
            Post post = new Post();
            if (postOption.Equals("blog"))
            {
                var data = await database.Read("Blogs", JsonConvert.SerializeObject(id));
                if (!string.IsNullOrEmpty(data.Value))
                {
                    var _post = JsonConvert.DeserializeObject<Post>(data.Value);
                    post = _post ?? new Post();
                }
            }
            else if (postOption.Equals("course"))
            {
                var data = await database.Read("Courses", JsonConvert.SerializeObject(id));
                if (!string.IsNullOrEmpty(data.Value))
                {
                    var _post = JsonConvert.DeserializeObject<Post>(data.Value);
                    post = _post ?? new Post();
                }
            }
            return post;
        }
        public async Task<List<Post>> GetAllPostFromDb(string postOption)
        {
            List<Post> posts = new List<Post>();
            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            if (postOption.Equals("blog"))
            {
                data = (await database.ReadAll("Blogs")).ToList();
            }
            else if (postOption.Equals("course"))
            {
                data = (await database.ReadAll("Courses")).ToList();
            }
            foreach (var post in data)
            {
                if (!string.IsNullOrEmpty(post.Value))
                {
                    var _post = JsonConvert.DeserializeObject<Post>(post.Value);
                    posts.Add(_post ?? new Post());
                }
            }
            return posts;
        }
        public async Task<bool> DeletePost(string id, string postOption)
        {
            bool res = false;
            if (postOption == "blog")
            {
                res = await database.Delete("Blogs", JsonConvert.SerializeObject(id));
            }
            else if (postOption == "course")
            {
                res = await database.Delete("Courses", JsonConvert.SerializeObject(id));
            }
            return res;
        }
        public async void AddLikes(string Tablename, string key)//works on per post
        {
            await database.Create("Likes/"+Tablename, JsonConvert.SerializeObject(key),JsonConvert.SerializeObject(true));
            //tablename reps the blog id, key reps the user Id
        }
        public async Task<bool> DoesUserLikeOnPostExist(string Tablename, string key)
        {
            var res = await database.Exists("Likes/" + Tablename, JsonConvert.SerializeObject(key));
            return res;
        }
        public async Task<int> GetAllLikes(string tablename)
        {
            var likes = (await database.ReadAll("Likes/"+tablename)).ToList();
            if (likes != null && likes.Count > 0)
            {
                return likes.Count;
            }
            return 0;
        }
        public async Task<bool> AddComments(Comment comment)
        {
            var res = await database.Create("Comments/" + comment.PostId, JsonConvert.SerializeObject(comment.Id), JsonConvert.SerializeObject(comment));
            return res;
        }
        public async Task<List<Comment>> GetAllComments(string postId)
        {
            List<Comment> res = new List<Comment>();
            var data = (await database.ReadAll("Comments/" + postId));
            if (data != null)
            {
                foreach (var comment in data)
                {
                    var _comment = JsonConvert.DeserializeObject<Comment>(comment.Value);
                    res.Add(_comment?? new Comment());
                }
            }
            return res;
        }


    }
}
