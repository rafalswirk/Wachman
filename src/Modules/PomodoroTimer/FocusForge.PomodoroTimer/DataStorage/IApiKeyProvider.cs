namespace FocusForge.PomodoroTimer.DataStorage
{
    public interface IApiKeyProvider
    {
        string GetKey();
        void SetKey(string key);
    }
}