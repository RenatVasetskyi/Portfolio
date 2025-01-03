namespace FactoryMethod
{
    public class PlayerControllerCreator : ControllerFactory
    {
        public override Controller Create()
        {
            return new PlayerController();
        }
    }
}