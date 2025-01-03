using System.Collections.Generic;
using Strategy.Example1.Interfaces;
using UnityEngine;

namespace Strategy.Example1
{
    public class PathFindingContext
    {
        private IPathFindingStrategy _pathFindingStrategy;

        public PathFindingContext() { }
        
        public PathFindingContext(IPathFindingStrategy pathFindingStrategy)
        {
            _pathFindingStrategy = pathFindingStrategy;
        }
        
        public void SetStrategy(IPathFindingStrategy pathFindingStrategy)
        {
            _pathFindingStrategy = pathFindingStrategy;
        }

        public List<Vector3> FindPath(Vector3 start, Vector3 end)
        {
            List<Vector3> path = _pathFindingStrategy.FindPath(start, end);
            
            Debug.Log($"Found path by {_pathFindingStrategy.GetType()}");

            return path;
        }
    }
}