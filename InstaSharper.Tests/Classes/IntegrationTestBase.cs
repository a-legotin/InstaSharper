using System;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Models.User;
using NUnit.Framework;

namespace InstaSharper.Tests.Integration
{
    [TestFixture]
    [Category("Integration")]
    public class IntegrationTestBase
    {
        protected virtual IUserCredentials GetUserCredentials() => new UserCredentials(
            Environment.GetEnvironmentVariable("instaapiusername"),
            Environment.GetEnvironmentVariable("instaapiuserpassword"));
    }

    [TestFixture]
    [Category("AlwaysRun")]
    public class UnitTestBase
    {
    }
}