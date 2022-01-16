﻿using System;

namespace InstaSharper.Abstractions.Models.User;

[Serializable]
public class InstaFriendshipShortStatus
{
    public long Pk { get; set; }

    public bool Following { get; set; } = false;

    public bool IsPrivate { get; set; }

    public bool IncomingRequest { get; set; }

    public bool OutgoingRequest { get; set; }

    public bool IsBestie { get; set; } = false;
}