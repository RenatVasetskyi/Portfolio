using AbstractFactory.Enemies;
using AbstractFactory.Enemies.Red;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AbstractFactory.Factories
{
    public class RedEnemyFactory : EnemyFactory
    {
        public override FireEnemy CreateFireEnemy()
        {
            RedFireEnemy enemyPrefab = Resources.Load<RedFireEnemy>(EnemyAssetPath.RedFireEnemy);
            
            RedFireEnemy enemy = Object.Instantiate(enemyPrefab);
            enemy.Initialize(10); //Can be logic which get parameters to initialize from data base

            return enemy;
        }

        public override MageEnemy CreateMageEnemy()
        {
            RedMageEnemy enemyPrefab = Resources.Load<RedMageEnemy>(EnemyAssetPath.RedMageEnemy);
            
            RedMageEnemy enemy = Object.Instantiate(enemyPrefab);
            enemy.Initialize(2); //Can be logic which get parameters to initialize from data base

            return enemy;
        }

        public override WaterEnemy CreateWaterEnemy()
        {
            RedWaterEnemy enemyPrefab = Resources.Load<RedWaterEnemy>(EnemyAssetPath.RedWaterEnemy);
            
            RedWaterEnemy enemy = Object.Instantiate(enemyPrefab);
            enemy.Initialize(120); //Can be logic which get parameters to initialize from data base

            return enemy;
        }
    }
}