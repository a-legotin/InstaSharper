# InstagramApi [InstaSharper]
Tokenless, butthurtless private API for Instagram. Get account information, media, explore tags and user feed without any applications and other crap.

## Development of this project postponed

### Before posting new issues: [Demo samples](https://github.com/a-legotin/InstaSharper/tree/develop/InstaSharper.Examples), [Tests project](https://github.com/a-legotin/InstaSharper/tree/develop/InstaSharper.Tests/Endpoints) and [Wiki page](https://github.com/a-legotin/InstaSharper/wiki/How-to-use-library-features)

Note that: there is a simple [Instagram API](https://github.com/a-legotin/InstagramAPI-Web) based on web-version of Instagram. This repository based on Instagram API for mobile devices.

[![Build status](https://ci.appveyor.com/api/projects/status/6os0fhi1awbplbka?svg=true)](https://ci.appveyor.com/project/a-legotin/instasharper)
[![Build status](https://travis-ci.org/a-legotin/InstaSharper.svg?branch=master)](https://travis-ci.org/a-legotin/InstaSharper)
[![NuGet](https://img.shields.io/nuget/v/InstaSharper.svg)](https://www.nuget.org/packages/InstaSharper/)
[![MyGet](https://img.shields.io/myget/a-legotin/v/instasharper-develop.svg)](https://www.myget.org/feed/Details/instasharper-develop)
[![GitHub stars](https://img.shields.io/github/stars/a-legotin/InstaSharper.svg)](https://github.com/a-legotin/InstaSharper/stargazers)

#### Current version: 1.4.0 [Stable], 1.5.0 [Under development]

## Overview
This project intends to provide all the features available in the Instagram API up to 12.0.0.7.91. It is being developed in C# for .NET Framework 4.5.2 and .NET Standard 2.0

#### This repository is provided for reference purposes only.

* Please note that this project is still in design and development phase; the libraries may suffer major changes even at the interface level, so don't rely (yet) in this software for production uses. *

## Cross-platform by design
Build with dotnet core. Can be used on Mac, Linux, Windows.

## Easy to install
Use library as dll, reference from [nuget](https://www.nuget.org/packages/InstaSharper/) or clone source code.
Pre-release version available at [myget feed](https://www.myget.org/feed/Details/instasharper-develop) together with [symbols](https://www.myget.org/F/instasharper-develop/symbols/)

## Features

Currently the library supports following coverage of the following Instagram APIs:

***

- [x] Login
- [x] Logout
- [x] Create new account
- [x] Get user explore feed
- [x] Get user timeline feed
- [x] Get all user media by username
- [x] Get media by its id
- [x] Get user info by its user name
- [x] Get current user info
- [x] Get tag feed by tag value
- [x] Get current user media
- [x] Get followers list
- [x] Get followers list for currently logged in user
- [x] Get following list
- [x] Get recent following activity
- [x] Get user tags by username
- [x] Get direct mailbox
- [x] Get recent recipients
- [x] Get ranked recipients
- [x] Get inbox thread
- [x] Get recent activity
- [x] Like media
- [x] Unlike media
- [x] Follow user
- [x] Unfollow user
- [x] Set account private
- [x] Set account public
- [x] Send comment
- [x] Delete comment
- [x] Upload photo
- [x] Upload video
- [x] Get followings list
- [x] Delete media (photo/video)
- [x] Upload story (photo)
- [x] Change password
- [x] Send direct message
- [x] Search location
- [x] Get location feed
- [x] Collection create/get by id/get all/add items


## Easy to use
#### Use builder to get Insta API instance:
```c#
var api = new InstaApiBuilder()
                .UseLogger(new SomeLogger())
                .UseHttpClient(new SomeHttpClient())
                .SetUser(new UserCredentials(...You user...))
                .UseHttpClient(httpHandlerWithSomeProxy)
                .Build();
```
##### Note: every API method has synchronous implementation as well

### Quick Examples
#### Login
```c#
IResult<bool> loggedIn = await api.LoginAsync();
```

#### Get user:
```c#
IResult<InstaUser> user = await api.GetUserAsync();
```

#### Get all user posts:
```c#
IResult<InstaMediaList> media = await api.GetUserMediaAsync();
```

#### Get media by its code:
```c#
IResult<InstaMedia> mediaItem = await api.GetMediaByIdAsync("1234567891234567891_123456789);
```

#### Get user timeline feed:
```c#
IResult<InstaFeed> feed = await api.GetUserFeedAsync();
```

#### Comment post:
```c#
IResult<bool> postResult = await apiInstance.CommentMediaAsync("1234567891234567891_123456789", "Hi there!");
```

##### for more samples you can look at [Examples folder](https://github.com/a-legotin/InstaSharper/tree/master/InstaSharper.Examples)


#### [Why two separate repos with same mission?](https://github.com/a-legotin/InstagramAPI-Web/wiki/Difference-between-API-Web-and-just-API-repositories)

#### [Wiki](https://github.com/a-legotin/InstagramAPI/wiki/)

## Special thanks

[ADeltaX](https://github.com/ADeltaX) for contribution

[vitalragaz](https://github.com/vitalragaz) for contribution

[n0ise9914](https://github.com/n0ise9914) for contribution

[Ramtinak](https://github.com/ramtinak) for contribution

[mgp25](https://github.com/mgp25) and his [php wrapper](https://github.com/mgp25/Instagram-API/)

# License

MIT

# Terms and conditions

- Anyone who uses this wrapper MUST follow [Instagram Policy](https://www.instagram.com/about/legal/terms/api/)
- Provided project MUST NOT be used for marketing purposes
- I will not provide support to anyone who wants this API to send massive messages/likes/follows and so on
- Use this API at your own risk

## Legal

This code is in no way affiliated with, authorized, maintained, sponsored or endorsed by Instagram or any of its affiliates or subsidiaries. This is an independent and unofficial API wrapper.
#### Code provided for reference purposes only.
