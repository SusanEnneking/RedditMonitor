# Reddit Monitor #
This project is a little .net Core exercise that monitors a subreddit topic and reports on:
- Posts with most up votes
- Users with most posts

## Information to help get the initial tokens needed ##
Before using this code, you'll need to create an app.  If you want to use the "Help me get tokens" method for getting tokens, the callback URL for your app needs to be https://not-an-aardvark.github.io/reddit-oauth-helper/.
\
To use Reddit.NET, you need your App Id, App secret and a token.  The info at the "Help me get tokens" link will explain how to get a long-lived token.\
[Help me get an app](https://www.reddit.com/prefs/apps)\
[Help me get tokens](https://docs.aws.amazon.com/solutions/latest/discovering-hot-topics-using-machine-learning/retrieve-and-manage-api-credentials-for-reddit-api-authentication.html)

## Nuget Packages ##
[Reddit.NET](https://github.com/sirkris/Reddit.NET)\
:bulb: Happy to have found Reddit.NET as most of the work is being done by that package.\
\
[DotNetEnv](https://www.nuget.org/packages/DotNetEnv/1.2.0)\
:warning: The package comments indicate .env should not be used for production deploys. This code will never make it to a production server, but fair warning!

## Helpful Links ##
[This singleton pattern example](https://www.c-sharpcorner.com/article/net-core-depedency-inject-with-real-use-case/) illustration is very helpful in understanding how the monitor class is working.\
[Reddit api docs](https://www.reddit.com/dev/api/) This code doesn't interact directly with the Reddit api.  Leaving that to Reddit.NET.