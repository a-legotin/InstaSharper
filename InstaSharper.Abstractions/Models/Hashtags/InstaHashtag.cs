﻿namespace InstaSharper.Abstractions.Models.Hashtags;

public class InstaHashtag
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long MediaCount { get; set; }
    public string ProfilePicUrl { get; set; }
}