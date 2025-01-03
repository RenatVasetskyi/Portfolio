using System.Collections.Generic;
using Strategy.Example1.Interfaces;
using UnityEngine;

namespace Strategy.Example1
{
    public class CarPathStrategy : IPathFindingStrategy
    {
        public List<Vector3> FindPath(Vector3 start, Vector3 end)
        {
            //Some logic
            return new List<Vector3>();
        }
    }
}