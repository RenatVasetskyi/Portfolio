using UnityEngine;

namespace Builder
{
    public class MobBuilder
    {
        private Mob _mobPrefab;
        private MobStat _stat;
        private string _name;
        private Sprite _skin;
        private MobSkin _mobSkin;
        
        public MobBuilder WithRootPrefab(Mob mobPrefab)
        {
            _mobPrefab = mobPrefab;
            
            return this;
        }
        
        public MobBuilder WithName(string name)
        {
            _name = name;
            
            return this;
        }
        
        public MobBuilder WithStats(MobStat stat)
        {
            _stat = stat;
            
            return this;
        }

        public MobBuilder WithSkin(MobSkin mobSkin, Sprite skin)
        {
            _mobSkin = mobSkin;
            _skin = skin;

            return this;
        }

        public Mob Build()
        {
            Mob createdMob = Object.Instantiate(_mobPrefab);
            MobSkin createdSkin = Object.Instantiate(_mobSkin, createdMob.transform);
            
            createdSkin.SetSkin(_skin);

            createdMob.SetName(_name);
            createdMob.SetStats(_stat);
            createdMob.SetSkin(_mobSkin);

            return createdMob;
        }

        public MobBuilder Reset()
        {
            _mobPrefab = null;
            _stat = null;
            _name = null;
            _skin = null;
            _mobSkin = null;

            return this;
        }
    }
}