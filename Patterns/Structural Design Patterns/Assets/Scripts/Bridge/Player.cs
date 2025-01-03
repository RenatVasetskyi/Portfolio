namespace Bridge
{
    public class Player
    {
        public void Move(Controller controller)
        {
            controller.Control();
        }
    }
}