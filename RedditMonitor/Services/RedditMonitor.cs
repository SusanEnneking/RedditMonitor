using System.Collections;
using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;

namespace Services;
public class RedditMonitor : IRedditMonitor
{
    readonly DateTime _monitorStartTime;
    readonly string? _subredditName;
    ICollection<Post> postsThisRun;

    public RedditMonitor(){
        DotNetEnv.Env.Load("../.env");
        var appId = Environment.GetEnvironmentVariable("REDDIT_APPID");
        var appSecret = Environment.GetEnvironmentVariable("REDDIT_SECRET");
        var refreshToken = Environment.GetEnvironmentVariable("REDDIT_REFRESH_TOKEN");
        var redditClient = new RedditClient(appId: appId, 
                    appSecret: appSecret, 
                    refreshToken: refreshToken);
        _subredditName = Environment.GetEnvironmentVariable("REDDIT_SUBREDDIT");
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
