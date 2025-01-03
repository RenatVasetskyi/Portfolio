using System;

namespace Code.GameInfrastructure.AllBaseServices.Interfaces
{
    public interface ICoinService
    {
        event Action OnCountChanged;
        int CoinsCount { get; }
        void GiveCoins(int count);
        void TakeCoins(int count);
        void LoadData();
    }
}