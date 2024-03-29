using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RedditMonitor.Models;
using RedditMonitor.Services;

namespace RedditMonitor.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IMonitor _monitorService;

    public HomeController(ILogger<HomeController> logger, IMonitor monitor)
    {
         _monitorService = monitor;
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Refreshes page data every 5 seconds.  Magic.
        this.HttpContext.Response.Headers.Add( "refresh", "5; url=" + Url.Action("index") );
        ViewData["MonitorStartTime"] = _monitorService.GetMonitorStartTime();
        ViewData["PostCount"] = _monitorService.GetPostCount();
        ViewData["SubRedditThread"] = _monitorService.GetSubredditName();
        ViewData["UserWithMostPosts"] = _monitorService.GetUserWithMostPosts();
        var postWithMostVotes = _monitorService.GetPostWithMostVotes();
        ViewData["MostUpvotedPost"] = postWithMostVotes?.Title;
        ViewData["MostUpvotedPostAuthor"] = postWithMostVotes?.Author;
        ViewData["MostUpvotedPostCount"] = postWithMostVotes?.UpVotes;
        return View();
    }

    public IActionResult Privacy()
    {

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
