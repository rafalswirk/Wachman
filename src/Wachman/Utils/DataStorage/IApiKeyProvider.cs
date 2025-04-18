namespace Wachman.Utils.DataStorage
{
    public interface IApiKeyProvider
    {
        string GetKey();
        void SetKey(string key);
    }
}