using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;

namespace RedditMonitor.Services;
public interface IMonitor
{

    public abstract DateTime GetMonitorStartTime();
    public abstract int GetPostCount();
    public abstract string? GetSubredditName();
    public abstract string? GetUserWithMostPosts();
    public abstract Post? GetPostWithMostVotes();

}
