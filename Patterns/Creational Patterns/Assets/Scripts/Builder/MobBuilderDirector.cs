using UnityEngine;

namespace Builder
{
    public class MobBuilderDirector : MonoBehaviour
    {
        [SerializeField] private Mob _mobPrefab;
        
        [SerializeField] private Sprite _defaultSkin;
        [SerializeField] private Sprite _betterSkin;

        [SerializeField] private MobSkin _mobSkin;

        private readonly MobStat _defaultMobStat = new MobStat()
        {
            Damage = 25,
            Hp = 100,
            Speed = 12
        };
        
        private readonly MobStat _betterMobStat = new MobStat()
        {
            Damage = 40,
            Hp = 150,
            Speed = 14
        };

        private void Awake()
        {
            CreateMobs();
        }

        private void CreateMobs()
        {
            MobBuilder mobBuilder = new MobBuilder();

            mobBuilder
                .Reset()
                .WithRootPrefab(_mobPrefab)
                .WithName("Default Mob")
                .WithStats(_defaultMobStat)
                .WithSkin(_mobSkin, _defaultSkin)
                .Build();
            
            mobBuilder
                .Reset()
                .WithRootPrefab(_mobPrefab)
                .WithName("Better Mob")
                .WithStats(_betterMobStat)
                .WithSkin(_mobSkin, _betterSkin)
                .Build();
        }
    }
}
