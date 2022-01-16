namespace InstaSharper.Abstractions.Models.Status;

public enum ResponseStatusType
{
    Unknown = 0,
    LoginRequired = 1,
    CheckPointRequired = 2,
    RequestsLimit = 3,
    SentryBlock = 4,
    OK = 5,
    WrongRequest = 6,
    SomePagesSkipped = 7,
    UnExpectedResponse = 8,
    InternalException = 9,
    ChallengeRequired = 10,
    ChallengeRequiredV2 = 10,
    InactiveUser = 11,
    ConsentRequired = 12,
    ActionBlocked = 13,
    Spam = 14,
    MediaNotFound = 15,
    CommentingIsDisabled = 16,
    AlreadyLiked = 17,
    DeletedPost = 18,
    CantLike = 19,
    NetworkProblem = 20,
    IPBlock = 21,
    UserNotFound = 22,
    PrivateMedia = 23,
    NoMediaMatch = 24,
    InternalError = 25
}