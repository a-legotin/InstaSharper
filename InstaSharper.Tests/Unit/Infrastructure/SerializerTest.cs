using System;
using System.IO;
using System.Net;
using InstaSharper.Abstractions.Models.User;
using InstaSharper.Abstractions.Models.UserState;
using InstaSharper.Models.Device;
using InstaSharper.Serialization;
using InstaSharper.Tests.Classes;
using InstaSharper.Utils;
using NUnit.Framework;

namespace InstaSharper.Tests.Unit.Infrastructure;

public class SerializerTest : UnitTestBase
{
    private class DummyObject
    {
        public string Value { get; set; }
    }

    [Test]
    public void JsonDeSerializationTest()
    {
        var serializer = new JsonSerializer();
        var deserialized = serializer.Deserialize<DummyObject>("{\"Value\":\"testvalue\"}");

        Assert.AreEqual(deserialized.Value, "testvalue");
    }

    [Test]
    public void JsonSerializationTest()
    {
        var dummy = new DummyObject
        {
            Value = "testvalue"
        };

        var serializer = new JsonSerializer();
        var serialized = serializer.Serialize(dummy);

        Assert.AreEqual("{\"Value\":\"testvalue\"}", serialized);
    }


    [Test]
    public void StreamSerializationTest()
    {
        var dummy = new UserState
        {
            Cookies = new CookieCollection(),
            Device = new AndroidDevice(Guid.NewGuid(), "my-device"),
            UserSession = new UserSession
            {
                CsrfToken = "token",
                RankToken = "rank-token",
                LoggedInUser = new InstaUserShort
                {
                    Pk = 1234,
                    FullName = "Test User",
                    UserName = "test-user"
                }
            }
        };

        var serializer = new StreamSerializer();
        var serialized = serializer.Serialize(dummy);

        var ms = new MemoryStream(serialized.ToByteArray());
        var deserialized = serializer.Deserialize<UserState>(ms);

        Assert.AreEqual(dummy.Device.DeviceId, deserialized.Device.DeviceId);
        Assert.AreEqual(dummy.UserSession.CsrfToken, deserialized.UserSession.CsrfToken);
        Assert.AreEqual(dummy.UserSession.LoggedInUser.Pk, deserialized.UserSession.LoggedInUser.Pk);
    }
}