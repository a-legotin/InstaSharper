using System;
using System.IO;
using System.Threading.Tasks;
using InstaSharper.Abstractions.API;
using InstaSharper.Abstractions.Models.User;
using NUnit.Framework;

namespace InstaSharper.Tests.Classes;

public class AuthenticatedTestBase : IntegrationTestBase
{
    [OneTimeSetUp]
    public async Task SetupFixture()
    {
        var credentials = GetUserCredentials();

        if (File.Exists(StateFile))
        {
            _api = Builder.Builder.Create()
                .WithUserCredentials(credentials)
                .WithUserSession(await File.ReadAllBytesAsync(StateFile))
                .Build();
            var me = await _api.User.GetUserAsync(credentials.Username);
            if (me.IsRight)
                return;
        }

        _api = Builder.Builder.Create()
            .WithUserCredentials(credentials)
            .Build();
        await DoLoginAndSaveState(credentials);
    }

    protected IInstaApi _api;
    private InstaUserShort _authenticatedUser;
    protected virtual string StateFile { get; } = Path.Combine(Path.GetTempPath(), "ig_state.bin");

    private async Task DoLoginAndSaveState(IUserCredentials userCredentials)
    {
        var result = await _api.User.LoginAsync();
        result.Match(ok =>
            {
                if (File.Exists(StateFile))
                    return;
                File.WriteAllBytes(StateFile, _api.User.GetUserSessionAsByteArray());
                _authenticatedUser = ok;
            },
            fail => throw new Exception($"Unable to authenticate user {userCredentials.Username}"));
    }
}