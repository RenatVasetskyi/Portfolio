using AbstractFactory.Enemies;
using AbstractFactory.Enemies.Blue;
using UnityEngine;

namespace AbstractFactory.Factories
{
    public class BlueEnemyFactory : EnemyFactory
    {
        public override FireEnemy CreateFireEnemy()
        {
            BlueFireEnemy enemyPrefab = Resources.Load<BlueFireEnemy>(EnemyAssetPath.BlueFireEnemy);
            
            BlueFireEnemy enemy = Object.Instantiate(enemyPrefab);
            enemy.Initialize(15); //Can be logic which get parameters to initialize from data base

            return enemy;
        }

        public override MageEnemy CreateMageEnemy()
        {
            BlueMageEnemy enemyPrefab = Resources.Load<BlueMageEnemy>(EnemyAssetPath.BlueMageEnemy);
            
            BlueMageEnemy enemy = Object.Instantiate(enemyPrefab);
            enemy.Initialize(3); //Can be logic which get parameters to initialize from data base

            return enemy;
        }

        public override WaterEnemy CreateWaterEnemy()
        {
            BlueWaterEnemy enemyPrefab = Resources.Load<BlueWaterEnemy>(EnemyAssetPath.BlueWaterEnemy);
            
            BlueWaterEnemy enemy = Object.Instantiate(enemyPrefab);
            enemy.Initialize(145); //Can be logic which get parameters to initialize from data base

            return enemy;
        }
    }
}