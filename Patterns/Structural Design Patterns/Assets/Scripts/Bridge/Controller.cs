namespace Bridge
{
    public class Controller
    {
        private readonly IDevice _device;

        public Controller(IDevice device)
        {
            _device = device;
        }

        public virtual void Control()
        {
            _device.Work();
        }
    }
}