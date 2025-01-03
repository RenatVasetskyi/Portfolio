namespace FactoryMethod
{
    public class EnemyControllerCreator : ControllerFactory
    {
        public override Controller Create()
        {
            return new EnemyController();
        }
    }
}