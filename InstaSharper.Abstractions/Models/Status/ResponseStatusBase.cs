namespace InstaSharper.Abstractions.Models.Status;

public abstract class ResponseStatusBase
{
    public virtual ResponseStatusType Status { get; protected set; }
    public virtual string Message { get; }
}