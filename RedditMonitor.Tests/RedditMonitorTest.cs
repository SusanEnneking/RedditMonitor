using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using RedditMonitor.Services;
namespace RedditMonitor.Tests;
public class RedditWrapperMock : IRedditWrapper{
    ICollection<Post> _postsThisRun;
    DateTime _monitorStartTime;
    string _subredditName;
    Subreddit _sub;

    public RedditWrapperMock(string appId, string appSecret, string refreshToken, string subredditName){
        _subredditName = subredditName;
        _postsThisRun = new List<Post>();
        Post post;
        for (int i = 0; i<10; i++){
            post = new Post(null, string.Empty);
            post.Author = string.Format("Author {0}", i);
            post.Title = string.Format("Title {0}", i);
            post.UpVotes = i;
            post.Subreddit = _subredditName;
            _postsThisRun.Add(post);
        }
        post = new Post(null, string.Empty);
        post.Author = "Author 8";
        post.Title = "Title 18";
        post.UpVotes = 4;
        post.Subreddit = _subredditName;
        _postsThisRun.Add(post);

    }

    public void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e){

    }   

    public void StartMonitoring(){
        _monitorStartTime = DateTime.Now;
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

public class Tests
{

    [Test]
    public void TestMonitor()
    {
        var redditWrapperMock = new RedditWrapperMock("AppId", "AppSecret", "refreshToken", "SubredditName");
        var monitor = new RedditMonitor.Services.Monitor(redditWrapperMock);
        var postCount = monitor.GetPostCount();
        Assert.That(postCount, Is.EqualTo(11));
        var mostUpvotedPost = monitor.GetPostWithMostVotes();
        Assert.That(mostUpvotedPost.Title, Is.EqualTo("Title 9"));
        Assert.That(mostUpvotedPost.UpVotes, Is.EqualTo(9));
        Assert.That(mostUpvotedPost.Author, Is.EqualTo("Author 9"));
        var monitorStartTime = monitor.GetMonitorStartTime();
        Assert.That(monitorStartTime, Is.EqualTo(redditWrapperMock.GetMonitorStartTime()));
        var subredditName = monitor.GetSubredditName();
        Assert.That(subredditName, Is.EqualTo(redditWrapperMock.GetSubredditName()));
        var userWithMostPosts = monitor.GetUserWithMostPosts();
        Assert.That(userWithMostPosts, Is.EqualTo("Author 8"));
        var postWithMostVotes = monitor.GetPostWithMostVotes();
        Assert.That(postWithMostVotes.Title, Is.EqualTo("Title 9"));
    }
}