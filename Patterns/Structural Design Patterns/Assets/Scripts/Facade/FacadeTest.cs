using UnityEngine;

namespace Facade
{
    public class FacadeTest : MonoBehaviour
    {
        private void Awake()
        {
            IWayFounder wayFounder = new WayFounder();
            
            wayFounder.Find();
        }
    }
}