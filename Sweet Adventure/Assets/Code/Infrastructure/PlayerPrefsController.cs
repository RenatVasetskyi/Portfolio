using Code.Infrastructure.Interfaces;
using UnityEngine;

namespace Code.Infrastructure
{
    public class PlayerPrefsController : IPlayerPrefsController
    {
        public void Int(string path, int @int)
        {
            PlayerPrefs.SetInt(path, @int);
            PlayerPrefs.Save();
        }
        
        public int Int(string path)
        {
            return PlayerPrefs.GetInt(path);
        }

        public void Bool(string path, bool @bool)
        {
            PlayerPrefs.SetInt(path, (@bool ? 1 : 0));
            PlayerPrefs.Save();
        }
        
        public bool Bool(string path)
        {
            return (PlayerPrefs.GetInt(path) != 0);
        }

        public bool Path(string path)
        {
            return PlayerPrefs.HasKey(path);
        }
    }
}