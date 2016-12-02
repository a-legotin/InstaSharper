# InstagramApi [InstaSharper]
Tokenless, butthurtless private API for Instagram. Get account information, media, explore tags and user feed without any applications and other crap.

Note that: there is a simple [Instagram API](https://github.com/a-legotin/InstagramAPI-Web) based on web-version of Instagram. This repository based on Instagram API for mobile devices.

[![Build status](https://ci.appveyor.com/api/projects/status/tgdu2w1xr2qmtmrh?svg=true)](https://ci.appveyor.com/project/a-legotin/instagramapi-xk3ds)
[![Build Status](https://travis-ci.org/a-legotin/InstagramAPI.svg?branch=master)](https://travis-ci.org/a-legotin/InstagramAPI)

#### Current version: 1.2.0 [Stable], 1.2.1 [Under development]

## Overview
This project intends to provide all the features available in the Instagram API up to v9.7.0. It is being developed in C# for .NET Framework 4.6 and .NET Standart 1.6

* Please note that this project is still in design and development phase; the libraries may suffer major changes even at the interface level, so don't rely (yet) in this software for production uses. *

## API Support

Currently the  library supports following coverage of the following Instagram APIs:
  * Login
  * Logout
  * Get user timeline feed
  * Get user media
  * Get media by its id
  * Get tag feed


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
                .SetUser(new UserCredentials(...You user...))
                .Build();
```
##### Note: every API method has Async implementation as well

### Quick Examples
#### Login
```c#
bool loggedIn = api.Login();
```

#### Get user:
```c#
InstaUser user = api.GetUser();
```

#### Get all user posts:
```c#
InstaMediaList media = api.GetUserMedia();
```

#### Get media by its code:
```c#
InstaMedia mediaItem = api.GetMediaByCode(mediaCode);
```

#### Get user timeline feed:
```c#
InstaFeed feed = api.GetUserFeed();
```

#### [Why two separate repos with same mission?](https://github.com/a-legotin/InstagramAPI-Web/wiki/Difference-between-API-Web-and-just-API-repositories)

#### [Wiki](https://github.com/a-legotin/InstagramAPI/wiki/)

# License

MIT

# Terms and conditions

- Anyone who uses this wrapper MUST follow [Instagram Policy](https://www.instagram.com/about/legal/terms/api/)
- Provided project MUST NOT be used for marketing purposes
- I will not provide support to anyone who wants this API to send massive messages/likes/follows and so on
- Use this API at your own risk

## Legal

This code is in no way affiliated with, authorized, maintained, sponsored or endorsed by Instagram or any of its affiliates or subsidiaries. This is an independent and unofficial API wrapper.
