using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKBlog.Models
{
    public class Comment
    {
        public string Username { get; set; } = string.Empty;
        public string Filepath { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string PostId { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }
}
