using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Architecture.Services.Interfaces
{
    public interface ICoroutineRunner 
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}