using System;

namespace InstaSharper.Abstractions.Models.Broadcast;

public class InstaBroadcastLike
{
    public int Likes { get; set; }

    public int BurstLikes { get; set; }

    public DateTime LikeTime { get; set; }
}