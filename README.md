# InstagramApi
Tokenless, butthurtless private API for Instagram. Get account information, media, explore tags and user feed without any applications and other crap.
This wrapper provides basic media from instagram, some of them even without authorization.
Note that: there is a simple [Instagram API](https://github.com/a-legotin/InstagramAPI-Web) based on web-version of Instagram. This repository based on Instagram API for mobile devices.

[![Build status](https://ci.appveyor.com/api/projects/status/tgdu2w1xr2qmtmrh?svg=true)](https://ci.appveyor.com/project/a-legotin/instagramapi-xk3ds)
[![Build Status](https://travis-ci.org/a-legotin/Instagram-API.svg?branch=master)](https://travis-ci.org/a-legotin/Instagram-API)

#### Current version: 1.0.0 [Under development]

#### [Why two separate repos with same mission?](https://github.com/a-legotin/InstagramAPI-Web/wiki/Difference-between-API-Web-and-just-API-repositories)

## Cross-platform by design
Build with dotnet core. Can be used on Mac, Linux, Windows.

## Easy to install
Use library as dll, reference from nuget or clone source code.

## Easy to use
#### Use builder to get Insta API instance:
```c#
var api = new InstaApiBuilder()
                .UseLogger(new SomeLogger())
                .UseHttpClient(new SomeHttpClient())
                .SetUserName(SomeUsername)
                .Build();
```
##### Note: every API method has Async implementation as well
#### Get user:
```c#
InstaUser user = api.GetUser();
```

#### Get all user posts:
```c#
InstaPostList posts = api.GetUserPosts();
```

#### Get media by its code:
```c#
InstaMedia mediaItem = api.GetMediaByCode();
```

# License

MIT

# Terms and conditions

- Provided project MUST NOT be used for marketing purposes
- I will not provide support to anyone who wants this API to send massive messages/likes/follows and so on
- Use this API at your own risk

## Legal

This code is in no way affiliated with, authorized, maintained, sponsored or endorsed by Instagram or any of its affiliates or subsidiaries. This is an independent and unofficial API.
