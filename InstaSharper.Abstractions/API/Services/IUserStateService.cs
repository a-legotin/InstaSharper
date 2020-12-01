namespace InstaSharper.Abstractions.API.Services
{
    public interface IUserStateService
    {
        byte[] GetStateDataAsByteArray();
        void LoadStateDataFromByteArray(byte[] bytes);
    }
}