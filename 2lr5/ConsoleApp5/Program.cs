using ConsoleApp5;
using static ConsoleApp5.Posts;

// GET
var posts = await Posts.GetPosts();

Console.WriteLine("GET: ");
foreach (var post in posts)
{
    Console.WriteLine("Id: " + post.id);
    Console.WriteLine("Title: " + post.title);
    Console.WriteLine("Description: " + post.body);
    Console.WriteLine();
}

// POST
Console.WriteLine("POST: ");
var postData = new Posts.Post("Some title", "Some body was told me");
var result = await Posts.CreatePost(postData);
foreach (var post in result)
{
    Console.WriteLine("Id: " + post.id);
    Console.WriteLine("Title: " + post.title);
    Console.WriteLine("Description: " + post.body);
    Console.WriteLine();
}

