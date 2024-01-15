using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;

namespace Services;
public interface IRedditMonitor
{
    // Using monitor example from Reddit.DOTNET examples
    // https://github.com/sirkris/Reddit.NET/blob/master/src/Example/Program.cs
    public abstract void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e);
    public abstract DateTime GetMonitorStartTime();
    public abstract int GetPostCount();
    public abstract string? GetSubredditName();
    public abstract string? GetUserWithMostPosts();
    public abstract Post? GetPostWithMostVotes();

}
