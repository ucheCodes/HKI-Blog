using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKBlog.Models
{
    public class Post
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Filepath { get; set; } = string.Empty;
        public string PostOption { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Price { get; set; }
    }
}
