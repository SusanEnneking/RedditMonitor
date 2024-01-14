using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;

namespace Services;
public class RedditMonitor : IRedditMonitor
{

    static RedditMonitor(){
        DotNetEnv.Env.Load("../.env");
        var appId = Environment.GetEnvironmentVariable("REDDIT_APPID");
        var appSecret = Environment.GetEnvironmentVariable("REDDIT_SECRET");
        var refreshToken = Environment.GetEnvironmentVariable("REDDIT_REFRESH_TOKEN");
        var redditClient = new RedditClient(appId: appId, 
                    appSecret: appSecret, 
                    refreshToken: refreshToken);
        Subreddit sub = redditClient.Subreddit("AskReddit").About();
        sub.Posts.NewUpdated += C_NewPostsUpdated;
        MonitorStartTime = DateTime.Now;
        sub.Posts.MonitorNew();
    }

    public static DateTime MonitorStartTime;
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
