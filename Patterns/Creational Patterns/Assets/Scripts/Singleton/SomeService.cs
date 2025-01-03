namespace Singleton
{
    public class SomeService
    {
        public static SomeService Instance { get { return _instance ??= new SomeService(); } }

        private static SomeService _instance;

        private SomeService() { }
    }
}