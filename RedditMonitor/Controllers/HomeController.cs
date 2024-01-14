using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RedditMonitor.Models;
using Services;

namespace RedditMonitor.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IRedditMonitor _monitorService;

    public HomeController(ILogger<HomeController> logger, IRedditMonitor monitor)
    {
         _monitorService = monitor;
        _logger = logger;
    }

    public IActionResult Index()
    {
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
