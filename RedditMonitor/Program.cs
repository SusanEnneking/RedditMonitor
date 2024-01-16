using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Reddit;
using Reddit.Things;
using RedditMonitor.Services;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load("../.env");
var appId = Environment.GetEnvironmentVariable("REDDIT_APPID");
var appSecret = Environment.GetEnvironmentVariable("REDDIT_SECRET");
var refreshToken = Environment.GetEnvironmentVariable("REDDIT_REFRESH_TOKEN");
var subredditName = Environment.GetEnvironmentVariable("REDDIT_SUBREDDIT");
var redditWrapper = new RedditWrapper(
    appId??string.Empty, 
    appSecret??string.Empty, 
    refreshToken??string.Empty, 
    subredditName??string.Empty
);
var monitor = new RedditMonitor.Services.Monitor(redditWrapper);
// Add services to the container.
builder.Services.AddSingleton<IMonitor>(monitor);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
