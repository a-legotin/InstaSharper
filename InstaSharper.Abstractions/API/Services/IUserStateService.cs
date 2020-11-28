using System.IO;

namespace InstaSharper.Abstractions.API.Services
{
    public interface IUserStateService
    {
        Stream GetStateDataAsStream();
        void LoadStateDataFromStream(Stream stream);
    }
}