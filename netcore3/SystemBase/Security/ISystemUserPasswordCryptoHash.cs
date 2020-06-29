namespace SystemBase.Security
{
    public interface ISystemUserPasswordCryptoHash
    {
        string ComputeHash(string password);
    }
}
