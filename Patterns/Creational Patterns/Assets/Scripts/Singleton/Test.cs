using UnityEngine;

namespace Singleton
{
    public class Test : MonoBehaviour
    {
        private void Awake()
        {
            TestSingleton();
        }

        private void TestSingleton()
        {
            SomeService service1 = SomeService.Instance;
            SomeService service2 = SomeService.Instance;

            Debug.Log(service1.GetHashCode() == service2.GetHashCode() ? "Singleton works" : "Singleton doesnt work");
        }
    }
}