﻿namespace InstaSharper.Classes.Models
{
    public class InstaBaseFeed : IInstaBaseList
    {
        public InstaMediaList Medias { get; set; } = new InstaMediaList();
        public string NextId { get; set; }
    }
}