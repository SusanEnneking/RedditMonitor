using Reddit.Controllers;

namespace Services;
public class RedditMonitor : IRedditMonitor
{
    IRedditWrapper _redditWrapper;
    public RedditMonitor(IRedditWrapper redditWrapper){
        _redditWrapper = redditWrapper;
        _redditWrapper.StartMonitoring();
    }

    public DateTime GetMonitorStartTime(){
        return _redditWrapper.GetMonitorStartTime();
    }

    public int GetPostCount(){
        return _redditWrapper.GetPosts().Count;
    }

    public string? GetSubredditName(){
        return _redditWrapper.GetSubredditName();
    }

    public string? GetUserWithMostPosts(){
        return _redditWrapper.GetPosts()
            .GroupBy(post => post.Author)
            .Select(post => new { Author = post.Key, Count = post.Count() })
            .OrderByDescending(post => post.Count)
            .Select(post => post.Author)
            .FirstOrDefault();
    }

    public Post? GetPostWithMostVotes(){
        return _redditWrapper.GetPosts()
            .OrderByDescending(post => post.UpVotes)
            .FirstOrDefault();
    }

}
