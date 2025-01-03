using UnityEngine;

namespace Architecture.Services.Interfaces
{
    public interface ICameraProvider
    {
        Camera Camera { get; }
        Vector2 GetLeftCameraCorner();
        Vector2 GetRightCameraCorner();
    }
}