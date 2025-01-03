using Assets.Scripts.Architecture.Services.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Architecture.Services
{
    public class AssetProvider : IAssetProvider
    {
        public T Initialize<T>(string path) where T : Object =>
            Resources.Load<T>(path);
    }
}