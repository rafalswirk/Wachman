namespace Wachman.Utils.DataStorage
{
    internal interface IApiKeyProvider
    {
        string GetKey();
        void SetKey(string key);
    }
}