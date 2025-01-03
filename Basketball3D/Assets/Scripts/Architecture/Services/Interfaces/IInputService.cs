using System;
using UnityEngine;

namespace Assets.Scripts.Architecture.Services.Interfaces
{
    public interface IInputService
    {
        event Action<Vector2> OnScreenTouched;
        event Action<Vector2> OnScreenTouchEnded;
    }
}