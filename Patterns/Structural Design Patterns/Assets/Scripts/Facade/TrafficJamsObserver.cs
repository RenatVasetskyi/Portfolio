using System.Collections.Generic;
using System.Linq;

namespace Facade
{
    public class TrafficJamsObserver
    {
        public Road GetEmptyRoad(List<Road> roads)
        {
            return Sort(roads).FirstOrDefault();
        }

        private List<Road> Sort(List<Road> roads)
        {
            return roads.OrderBy(x=>x.JamsPercent).ToList();
        }
    }
}