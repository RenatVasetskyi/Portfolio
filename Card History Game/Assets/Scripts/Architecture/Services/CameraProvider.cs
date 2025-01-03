using Architecture.Services.Interfaces;
using UnityEngine;

namespace Architecture.Services
{
    public class CameraProvider : ICameraProvider
    {
        public Camera Camera { get; private set; }

        public Vector2 GetLeftCameraCorner()
        {
            SetCamera();
            
            return Camera.ViewportToWorldPoint(new Vector3(0, 0, Camera.nearClipPlane));
        }

        public Vector2 GetRightCameraCorner()
        {
            SetCamera();
            
            return Camera.ViewportToWorldPoint(new Vector3(1, 1, Camera.nearClipPlane));
        }
        
        private void SetCamera()
        {
            if (Camera == null)
                Camera = Camera.main;
        }
    }
}