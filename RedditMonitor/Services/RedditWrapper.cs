using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;

namespace RedditMonitor.Services;

public class RedditWrapper : IRedditWrapper
{
    ICollection<Post> _postsThisRun;
    DateTime _monitorStartTime;
    string _subredditName;
    Subreddit _sub;

    public RedditWrapper(string appId, string appSecret, string refreshToken, string subredditName){
        var redditClient = new RedditClient(appId: appId, 
            appSecret: appSecret, 
            refreshToken: refreshToken);
        _subredditName = subredditName;
        _sub = redditClient.Subreddit(_subredditName).About();
        _sub.Posts.NewUpdated += C_NewPostsUpdated;
        _postsThisRun = new List<Post>();
    }

    // Using monitor example from Reddit.DOTNET examples
    // https://github.com/sirkris/Reddit.NET/blob/master/src/Example/Program.cs
    public void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e){
        foreach (Post post in e.Added)
        {
            _postsThisRun.Add(post);
            Console.WriteLine(string.Format("[{0}] New Post by {1} : {2}", post.Subreddit, post.Author, post.Title));
        }
    }   

    public void StartMonitoring(){
        _monitorStartTime = DateTime.Now;
        _sub.Posts.MonitorNew();
    }
    public DateTime GetMonitorStartTime(){
        return _monitorStartTime;
    }

    public ICollection<Post> GetPosts(){
        return _postsThisRun;
    }
    public string GetSubredditName(){
        return _subredditName;
    }

}
