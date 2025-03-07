using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;

namespace Code.MainInfrastructure.MainGameService
{
    public class SaveToPlayerPrefs : ISaveToPlayerPrefs
    {
        public void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, (value ? 1 : 0));
            PlayerPrefs.Save();
        }

        public void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        
        public int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public bool GetBool(string key)
        {
            return (PlayerPrefs.GetInt(key) != 0);
        }

        public float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}