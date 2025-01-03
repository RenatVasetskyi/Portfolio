using System.Collections.Generic;
using UnityEngine;

namespace Facade
{
    public class WayFounder : IWayFounder
    {
        public void Find()
        {
            NavigationSystem navigationSystem = new NavigationSystem();
            
            List<Road> shortestRoads = navigationSystem.CalculateShortestWay(new DataBase());

            TrafficJamsObserver trafficJamsObserver = new TrafficJamsObserver();
            
            Road shortestWay = trafficJamsObserver.GetEmptyRoad(shortestRoads);

            Debug.Log($"Way found, Distance: {shortestWay.Distance}, Jams percent {shortestWay.JamsPercent}");
        }
    }
}