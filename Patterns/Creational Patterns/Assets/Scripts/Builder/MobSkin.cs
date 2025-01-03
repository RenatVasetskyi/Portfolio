using UnityEngine;

namespace Builder
{
    public class MobSkin : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetSkin(Sprite skin)
        {
            _spriteRenderer.sprite = skin;
        }
    }
}