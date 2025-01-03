using System;

namespace Assets.Scripts.Architecture.Services.Interfaces
{
    public interface ISceneLoader 
    {
        void Load(string nextScene, Action onLoaded = null);
    }
}