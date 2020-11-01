using System.Threading.Tasks;

namespace InstaSharper.Infrastructure
{
    internal interface ILauncherKeysProvider
    {
        Task<(string publicKey, string keyId)> GetKeysAsync();
    }
}