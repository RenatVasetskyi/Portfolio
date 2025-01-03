using UnityEngine;

namespace Strategy.Example1
{
    public class StrategyTest : MonoBehaviour
    {
        private void Awake()
        {
            PathFindingContext pathFindingContext = new PathFindingContext(new CarPathStrategy());
            pathFindingContext.FindPath(Vector3.down, Vector3.up);
            pathFindingContext.SetStrategy(new BikePathStrategy());
            pathFindingContext.FindPath(Vector3.down, Vector3.up);
        }
    }
}