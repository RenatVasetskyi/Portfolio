using AbstractFactory.Enemies;

namespace AbstractFactory.Factories
{
    public abstract class EnemyFactory
    {
        public abstract FireEnemy CreateFireEnemy();
        public abstract MageEnemy CreateMageEnemy();
        public abstract WaterEnemy CreateWaterEnemy();
    }
}