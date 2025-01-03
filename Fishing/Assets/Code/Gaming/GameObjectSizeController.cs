using UnityEngine;

namespace Code.Gaming
{
    public class GameObjectSizeController : MonoBehaviour
    {
        [SerializeField] private float _scaleMultiplier = 5f;

        private void Awake()
        {
            Control();
        }

        private void Control()
        {
            float needAspect = 19.5f / 9f;
            float myAspect = (float)Screen.width / Screen.height;
            float scale = myAspect / needAspect * _scaleMultiplier;
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        }
    }
}