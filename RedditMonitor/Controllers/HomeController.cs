using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RedditMonitor.Models;

namespace RedditMonitor.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private Services.RedditMonitor _monitorService;

    public HomeController(ILogger<HomeController> logger)
    {
         _monitorService = new Services.RedditMonitor();
        _logger = logger;
    }

    public IActionResult Index()
    {
       
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
