using UnityEngine;

namespace Assets.Scripts.Data
{
    public static class Extensions
    {
        public static Vector3 ToWorldPosition(this Vector2 vector, Camera camera) =>
            camera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, camera.transform.position.z));

        public static Vector2 ToScreenPosition(this Vector3 vector, Camera camera) =>
            camera.WorldToScreenPoint(vector);
    }
}