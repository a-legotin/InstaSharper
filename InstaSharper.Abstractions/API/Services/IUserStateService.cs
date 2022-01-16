namespace InstaSharper.Abstractions.API.Services;

public interface IUserStateService
{
    bool IsAuthenticated { get; }
    byte[] GetStateDataAsByteArray();
    void LoadStateDataFromByteArray(byte[] bytes);
}