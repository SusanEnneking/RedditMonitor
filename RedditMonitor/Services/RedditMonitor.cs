using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;

namespace Services;
public class RedditMonitor : IRedditMonitor
{
    readonly DateTime _monitorStartTime;
    readonly string? _subredditName;
    ICollection<Post> postsThisRun;

    public RedditMonitor(RedditClient redditClient, string subredditName){
        _subredditName = subredditName;
        Subreddit sub = redditClient.Subreddit(_subredditName).About();
        sub.Posts.NewUpdated += C_NewPostsUpdated;
        postsThisRun = new List<Post>();
        _monitorStartTime = DateTime.Now;
        sub.Posts.MonitorNew();
    }

    public DateTime GetMonitorStartTime(){
        return _monitorStartTime;
    }

    public int GetPostCount(){
        return postsThisRun.Count();
    }

    public string? GetSubredditName(){
        return _subredditName;
    }

    public string? GetUserWithMostPosts(){
        return postsThisRun
            .GroupBy(post => post.Author)
            .Select(post => new { Author = post.Key, Count = post.Count() })
            .OrderByDescending(post => post.Count)
            .Select(post => post.Author)
            .FirstOrDefault();
    }

    public Post? GetPostWithMostVotes(){
        return postsThisRun
            .OrderByDescending(post => post.UpVotes)
            .FirstOrDefault();
    }
    // Using monitor example from Reddit.DOTNET examples
    // https://github.com/sirkris/Reddit.NET/blob/master/src/Example/Program.cs
    public void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e)
    {
        foreach (Post post in e.Added)
        {
            postsThisRun.Add(post);
            Console.WriteLine("[" + post.Subreddit + "] New Post by " + post.Author + ": " + post.Title);
        }
    }

}
