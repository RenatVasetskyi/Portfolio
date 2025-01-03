using System.Collections;
using UnityEngine;

namespace Code.Infrastructure.Interfaces
{
    public interface IUnityCoroutineProvider
    {
        Coroutine Provide(IEnumerator provide);
        void StopProviding(Coroutine stopProviding);
    }
}