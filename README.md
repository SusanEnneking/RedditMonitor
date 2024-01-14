# Reddit Monitor #
This project is a little .net Core exercise that monitors a subreddit topic and reports on:
- Posts with most up votes
- Users with most posts

## Information to help get the initial tokens needed ##
[Help me get an app](https://www.reddit.com/prefs/apps)\
[Help me get tokens](https://docs.aws.amazon.com/solutions/latest/discovering-hot-topics-using-machine-learning/retrieve-and-manage-api-credentials-for-reddit-api-authentication.html)

## Nuget Packages ##
[Reddit.NET](https://github.com/sirkris/Reddit.NET)\
:bulb: Happy to have found Reddit.NET as most of the work is being done by that package.\
\
[DotNetEnv](https://www.nuget.org/packages/DotNetEnv/1.2.0)\
:warning: The package comments indicate .env should not be used for production deploys. This code will never make it to a production server, but fair warning!

## Helpful Links ##
[This singleton pattern example](https://www.c-sharpcorner.com/article/net-core-depedency-inject-with-real-use-case/) illustration is very helpful in understanding how the monitor class is working.