using UnityEngine;
using Object = UnityEngine.Object;

namespace ObjectPool
{
    public class GameObjectPool : BasePool<GameObject>
    {
        public GameObjectPool(GameObject prefab, Transform parent, int preloadCount) :
            base(() => Preload(prefab, parent), Get, Return, preloadCount) { }
        
        private static GameObject Preload(GameObject prefab, Transform parent)
        {
            return Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        }

        private new static void Return(GameObject bullet)
        {
            bullet.gameObject.SetActive(false);
            
            bullet.transform.position = Vector3.zero;
        }
        
        private static void Get(GameObject bullet)
        {
            bullet.gameObject.SetActive(true);
        }
    }
}