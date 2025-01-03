using System.Collections;
using UnityEngine;

namespace Code.MainInfrastructure.MainGameService.Interfaces
{
    public interface ICoroutinePusher
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}