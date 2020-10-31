using InstaSharper.Abstractions.API.Actions;

namespace InstaSharper.Abstractions.API
{
    public interface IInstaApi
    {
        public IUserActions User { get; }
    }
}