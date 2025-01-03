using UnityEngine;

namespace Code.Game
{
    public class GetBounds
    {
        public void Get(Camera camera, out Vector2 screenBounds, out float myWidth, MeshRenderer meshRenderer)
        {
            screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
            myWidth = meshRenderer.bounds.extents.x;
        }
        
        public void Get(Camera camera, out Vector2 screenBounds, out float myWidth, SpriteRenderer spriteRenderer)
        {
            screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
            myWidth = spriteRenderer.bounds.extents.x;
        }
    }
}