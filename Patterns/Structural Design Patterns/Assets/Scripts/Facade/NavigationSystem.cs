using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Facade
{
    public class NavigationSystem
    {
        private const int MaxDistance = 730;
        
        public List<Road> CalculateShortestWay(DataBase dataBase)
        {
            dataBase.Connect();
            dataBase.GetAllRoads();

            var roads = new List<Road>(new []
            {
                new Road(1, 540),
                new Road(4, 860),
                new Road(8, 770),
                new Road(2, 430),
                new Road(76, 135),
                new Road(22, 654),
            });
            
            return Sort(roads);
        }

        private List<Road> Sort(List<Road> roads)
        {
            Debug.Log("Roads sorted");
            
            return roads.Where(x => x.Distance <= MaxDistance).OrderBy(x => x.Distance).ToList();
        }
    }
}