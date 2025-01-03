namespace Code.MainInfrastructure.MainGameService.Interfaces
{
    public interface ISaveToPlayerPrefs
    {
        void SetInt(string key, int value);
        void SetBool(string key, bool value);
        void SetFloat(string key, float value);
        void SetString(string key, string value);
        int GetInt(string key);
        bool GetBool(string key);
        float GetFloat(string key);
        string GetString(string key);
        bool HasKey(string key);
        void DeleteKey(string key);
    }
}