using System.Collections.Generic;
using UnityEngine;

namespace Strategy.Example1.Interfaces
{
    public interface IPathFindingStrategy
    {
        List<Vector3> FindPath(Vector3 start, Vector3 end);
    }
}