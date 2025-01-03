namespace Code.Infrastructure.Interfaces
{
    public interface IPlayerPrefsController
    {
        void Int(string path, int @int);
        int Int(string path);
        void Bool(string path, bool @bool);
        bool Bool(string path);
        bool Path(string path);
    }
}