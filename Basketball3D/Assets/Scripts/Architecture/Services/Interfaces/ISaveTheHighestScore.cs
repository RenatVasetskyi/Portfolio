namespace Assets.Scripts.Architecture.Services.Interfaces
{
    public interface ISaveTheHighestScore
    {
        int TheHighestScore { get; }
        void Save();
        void Load();
    }
}