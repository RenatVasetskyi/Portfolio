using UnityEngine;

namespace Builder
{
    public class Mob : MonoBehaviour
    {
        public string Name { get; private set; }
        public MobStat Stat { get; private set; }
        
        private MobSkin _skin;

        public void SetName(string name)
        {
            Name = name;

            transform.name = name;
        }
        
        public void SetStats(MobStat stat)
        {
            Stat = stat;
        }
        
        public void SetSkin(MobSkin mobSkin)
        {
            _skin = mobSkin;
        }
    }
}