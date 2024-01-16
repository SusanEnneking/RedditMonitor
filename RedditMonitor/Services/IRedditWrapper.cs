using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;

namespace Services;
public interface IRedditWrapper
{
    public abstract void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e);
    public abstract void StartMonitoring();
    public abstract DateTime GetMonitorStartTime();
    public ICollection<Post> GetPosts();
    public string GetSubredditName();
}
