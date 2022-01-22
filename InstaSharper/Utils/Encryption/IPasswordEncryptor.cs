namespace InstaSharper.Utils.Encryption;

internal interface IPasswordEncryptor
{
    string EncryptPassword(string password,
                           string pubKey,
                           string pubKeyId,
                           long time);
}