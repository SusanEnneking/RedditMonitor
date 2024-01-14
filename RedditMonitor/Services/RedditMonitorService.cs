using System;
using Reddit;
namespace RedditMonitor.Services;


public class RedditMonitor
{
    string? _appId;
    string? _appSecret;
    string? _refreshToken;
    DateTime _monitorStarttime;

    public RedditMonitor(){
        _monitorStarttime = DateTime.Now;
        DotNetEnv.Env.Load("../.env");
        _appId = Environment.GetEnvironmentVariable("REDDIT_APPID");
        _appSecret = Environment.GetEnvironmentVariable("REDDIT_SECRET");
        _refreshToken = Environment.GetEnvironmentVariable("REDDIT_REFRESH_TOKEN");
        var redditClient = new RedditClient(appId: _appId, 
                    appSecret: _appSecret, 
                    refreshToken: _refreshToken);
        // Get info on another subreddit.
        var askReddit = redditClient.Subreddit("AskReddit").About();

        // Get the top post from a subreddit.
        var topPost = askReddit.Posts.Top[0];
    }
    public DateTime MonitorStartTime => _monitorStarttime;

}
