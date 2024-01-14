using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;

namespace RedditMonitor.Services;
public class RedditMonitor
{
    readonly string? _appId;
    readonly string? _appSecret;
    readonly string? _refreshToken;
    DateTime _monitorStarttime;

    public RedditMonitor(){
        DotNetEnv.Env.Load("../.env");
        _appId = Environment.GetEnvironmentVariable("REDDIT_APPID");
        _appSecret = Environment.GetEnvironmentVariable("REDDIT_SECRET");
        _refreshToken = Environment.GetEnvironmentVariable("REDDIT_REFRESH_TOKEN");
        var redditClient = new RedditClient(appId: _appId, 
                    appSecret: _appSecret, 
                    refreshToken: _refreshToken);
        Subreddit sub = redditClient.Subreddit("AskReddit").About();
        sub.Posts.NewUpdated += C_NewPostsUpdated;
        _monitorStarttime = DateTime.Now;
        sub.Posts.MonitorNew();
    }

    public DateTime MonitorStartTime => _monitorStarttime;
    // Using monitor example from Reddit.DOTNET examples
    // https://github.com/sirkris/Reddit.NET/blob/master/src/Example/Program.cs
    public static void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e)
    {
        foreach (Post post in e.Added)
        {
            Console.WriteLine("[" + post.Subreddit + "] New Post by " + post.Author + ": " + post.Title);
        }
    }

}
