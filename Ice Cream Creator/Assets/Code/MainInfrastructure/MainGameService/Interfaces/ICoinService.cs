using System;

namespace Code.MainInfrastructure.MainGameService.Interfaces
{
    public interface ICoinService
    {
        event Action OnChanged;
        int Coins { get; }
        void AddCoins(int count);
        void ReduceCoins(int count);
        void Load();
    }
}