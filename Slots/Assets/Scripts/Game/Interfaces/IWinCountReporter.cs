using System;

namespace Game.Interfaces
{
    public interface IWinCountReporter
    {
        event Action<int> OnWin;
        void SendWinCount(int count);
    }
}