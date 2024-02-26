using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Posts
    {
        readonly static string baseUrl = "https://jsonplaceholder.typicode.com/posts";

        public class Post(string title, string body)
        {
            public int? id { get; set; }
            public string? title { get; set; } = title;
            public string? body { get; set; } = body;
        }

        public static async Task<List<Post>?> GetPosts ()
        {
            var result = await Api.Get<Post>(baseUrl);
            return result.Data;
        }

        public static async Task<List<Post>> CreatePost (Post newPost)
        {
            var result = await Api.Post<Post>(baseUrl, newPost);
            return result.Data;
        }
    }
}
