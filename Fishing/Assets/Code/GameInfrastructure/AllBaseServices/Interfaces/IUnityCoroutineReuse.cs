using System.Collections;
using UnityEngine;

namespace Code.GameInfrastructure.AllBaseServices.Interfaces
{
    public interface IUnityCoroutineReuse
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}